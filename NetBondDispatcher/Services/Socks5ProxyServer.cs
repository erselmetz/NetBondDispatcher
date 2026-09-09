using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetBondDispatcher.Models;

namespace NetBondDispatcher.Services;

public enum ProxyBindingStrategy
{
    RoundRobin,
    LeastConnections
}

public class Socks5ProxyServer
{
    private TcpListener? _listener;
    private CancellationTokenSource? _cts;
    private readonly List<NetworkAdapterInfo> _activeAdapters = new();
    private readonly object _adapterLock = new();
    private readonly ConcurrentDictionary<IPAddress, int> _adapterSessions = new();
    private int _roundRobinIndex = 0;
    private int _activeConnections = 0;

    public bool IsRunning => _listener != null;
    public int Port { get; private set; } = 10808;
    public ProxyBindingStrategy Strategy { get; set; } = ProxyBindingStrategy.RoundRobin;
    public int ActiveConnections => _activeConnections;

    public event Action<string>? OnLog;
    public event Action<int>? OnActiveConnectionsChanged;
    public event Action<long, long>? OnTraffic; // bytesReceived, bytesSent
    public event Action<Dictionary<IPAddress, int>>? OnSessionDistributionChanged;

    public void UpdateAdapters(IEnumerable<NetworkAdapterInfo> adapters)
    {
        lock (_adapterLock)
        {
            _activeAdapters.Clear();
            _activeAdapters.AddRange(adapters);
        }
    }

    public Task StartAsync(int port, IEnumerable<NetworkAdapterInfo> adapters, ProxyBindingStrategy strategy = ProxyBindingStrategy.RoundRobin)
    {
        if (IsRunning)
            return Task.CompletedTask;

        Port = port;
        Strategy = strategy;
        UpdateAdapters(adapters);

        _cts = new CancellationTokenSource();
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();

        Log($"[SOCKS5] Server started on port {port} using {strategy} strategy.");
        _ = AcceptClientsAsync(_listener, _cts.Token);

        return Task.CompletedTask;
    }

    public void Stop()
    {
        if (!IsRunning)
            return;

        try
        {
            _cts?.Cancel();
            _listener?.Stop();
        }
        catch (Exception ex)
        {
            Log($"[SOCKS5] Error stopping listener: {ex.Message}");
        }
        finally
        {
            _listener = null;
            _cts?.Dispose();
            _cts = null;
            _adapterSessions.Clear();
            Interlocked.Exchange(ref _activeConnections, 0);
            OnActiveConnectionsChanged?.Invoke(0);
            OnSessionDistributionChanged?.Invoke(new Dictionary<IPAddress, int>());
            Log("[SOCKS5] Server stopped.");
        }
    }

    private async Task AcceptClientsAsync(TcpListener listener, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                var client = await listener.AcceptTcpClientAsync(token).ConfigureAwait(false);
                _ = HandleClientAsync(client, token);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                {
                    Log($"[SOCKS5] Accept error: {ex.Message}");
                }
                break;
            }
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken serverToken)
    {
        Interlocked.Increment(ref _activeConnections);
        OnActiveConnectionsChanged?.Invoke(_activeConnections);

        using var clientRegistration = client;
        using var clientStream = client.GetStream();
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(serverToken);
        var token = linkedCts.Token;

        Socket? targetSocket = null;
        NetworkAdapterInfo? assignedAdapter = null;

        try
        {
            // 1. SOCKS5 Handshake
            byte[] buffer = new byte[512];
            int read = await ReadExactAsync(clientStream, buffer, 2, token).ConfigureAwait(false);
            if (read < 2 || buffer[0] != 0x05)
                return;

            int numMethods = buffer[1];
            await ReadExactAsync(clientStream, buffer, numMethods, token).ConfigureAwait(false);

            // Respond: [VER = 0x05] [METHOD = 0x00 (NO AUTH)]
            await clientStream.WriteAsync(new byte[] { 0x05, 0x00 }, token).ConfigureAwait(false);

            // 2. SOCKS5 Request
            read = await ReadExactAsync(clientStream, buffer, 4, token).ConfigureAwait(false);
            if (read < 4 || buffer[0] != 0x05)
                return;

            byte cmd = buffer[1];
            byte atyp = buffer[3];

            if (cmd != 0x01) // 0x01 = CONNECT
            {
                await SendReplyAsync(clientStream, 0x07, token).ConfigureAwait(false); // Command not supported
                return;
            }

            string targetHost;
            IPAddress? targetIp = null;

            if (atyp == 0x01) // IPv4
            {
                await ReadExactAsync(clientStream, buffer, 4, token).ConfigureAwait(false);
                targetIp = new IPAddress(buffer.AsSpan(0, 4));
                targetHost = targetIp.ToString();
            }
            else if (atyp == 0x03) // Domain name
            {
                await ReadExactAsync(clientStream, buffer, 1, token).ConfigureAwait(false);
                int domainLen = buffer[0];
                await ReadExactAsync(clientStream, buffer, domainLen, token).ConfigureAwait(false);
                targetHost = Encoding.ASCII.GetString(buffer, 0, domainLen);
            }
            else if (atyp == 0x04) // IPv6
            {
                await ReadExactAsync(clientStream, buffer, 16, token).ConfigureAwait(false);
                targetIp = new IPAddress(buffer.AsSpan(0, 16));
                targetHost = targetIp.ToString();
            }
            else
            {
                await SendReplyAsync(clientStream, 0x08, token).ConfigureAwait(false);
                return;
            }

            await ReadExactAsync(clientStream, buffer, 2, token).ConfigureAwait(false);
            ushort targetPort = BinaryPrimitives.ReadUInt16BigEndian(buffer.AsSpan(0, 2));

            // Select outgoing adapter according to configured strategy
            assignedAdapter = SelectAdapter();
            if (assignedAdapter != null)
            {
                _adapterSessions.AddOrUpdate(assignedAdapter.IpAddress, 1, (_, count) => count + 1);
                NotifySessionDistribution();
            }

            string adapterNotice = assignedAdapter != null
                ? $"via [{assignedAdapter.IpAddress}] ({assignedAdapter.Name})"
                : "via default route";

            Log($"[CONNECT] {targetHost}:{targetPort} {adapterNotice}");

            // Resolve target IP if domain
            if (targetIp == null)
            {
                try
                {
                    var addresses = await Dns.GetHostAddressesAsync(targetHost, token).ConfigureAwait(false);
                    targetIp = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
                               ?? addresses.FirstOrDefault();

                    if (targetIp == null)
                    {
                        Log($"[FAILED] Cannot resolve host '{targetHost}'");
                        await SendReplyAsync(clientStream, 0x04, token).ConfigureAwait(false);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Log($"[FAILED] DNS lookup failed for {targetHost}: {ex.Message}");
                    await SendReplyAsync(clientStream, 0x04, token).ConfigureAwait(false);
                    return;
                }
            }

            // Create outbound socket and bind to selected adapter
            targetSocket = new Socket(targetIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = true
            };

            if (assignedAdapter != null && assignedAdapter.IpAddress.AddressFamily == targetIp.AddressFamily)
            {
                try
                {
                    targetSocket.Bind(new IPEndPoint(assignedAdapter.IpAddress, 0));
                }
                catch (Exception ex)
                {
                    Log($"[WARN] Bind failed on {assignedAdapter.IpAddress}: {ex.Message}. Falling back.");
                }
            }

            try
            {
                using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                using var connectCts = CancellationTokenSource.CreateLinkedTokenSource(token, timeoutCts.Token);
                await targetSocket.ConnectAsync(new IPEndPoint(targetIp, targetPort), connectCts.Token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log($"[FAILED] Connection to {targetHost}:{targetPort} failed: {ex.Message}");
                await SendReplyAsync(clientStream, 0x05, token).ConfigureAwait(false);
                return;
            }

            // Send SOCKS5 Success reply
            byte[] successReply = new byte[10];
            successReply[0] = 0x05;
            successReply[1] = 0x00; // Success
            successReply[2] = 0x00;
            successReply[3] = 0x01; // IPv4

            if (targetSocket.LocalEndPoint is IPEndPoint localEp && localEp.AddressFamily == AddressFamily.InterNetwork)
            {
                localEp.Address.GetAddressBytes().CopyTo(successReply, 4);
                BinaryPrimitives.WriteUInt16BigEndian(successReply.AsSpan(8, 2), (ushort)localEp.Port);
            }

            await clientStream.WriteAsync(successReply, token).ConfigureAwait(false);

            // 3. Bi-directional relay
            using var targetStream = new NetworkStream(targetSocket, ownsSocket: true);
            var clientToTarget = RelayTrafficAsync(clientStream, targetStream, token, isUpload: true);
            var targetToClient = RelayTrafficAsync(targetStream, clientStream, token, isUpload: false);

            await Task.WhenAny(clientToTarget, targetToClient).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // Disconnection
        }
        catch (Exception ex)
        {
            Log($"[SOCKS5] Client error: {ex.Message}");
        }
        finally
        {
            if (assignedAdapter != null)
            {
                _adapterSessions.AddOrUpdate(assignedAdapter.IpAddress, 0, (_, count) => Math.Max(0, count - 1));
                NotifySessionDistribution();
            }

            targetSocket?.Dispose();
            Interlocked.Decrement(ref _activeConnections);
            OnActiveConnectionsChanged?.Invoke(_activeConnections);
        }
    }

    private void NotifySessionDistribution()
    {
        OnSessionDistributionChanged?.Invoke(new Dictionary<IPAddress, int>(_adapterSessions));
    }

    private async Task RelayTrafficAsync(Stream from, Stream to, CancellationToken token, bool isUpload)
    {
        byte[] buffer = new byte[16384];
        while (!token.IsCancellationRequested)
        {
            int bytesRead = await from.ReadAsync(buffer, token).ConfigureAwait(false);
            if (bytesRead <= 0)
                break;

            await to.WriteAsync(buffer.AsMemory(0, bytesRead), token).ConfigureAwait(false);

            if (isUpload)
                OnTraffic?.Invoke(0, bytesRead);
            else
                OnTraffic?.Invoke(bytesRead, 0);
        }
    }

    private static async Task SendReplyAsync(Stream stream, byte replyCode, CancellationToken token)
    {
        byte[] response = new byte[] { 0x05, replyCode, 0x00, 0x01, 0, 0, 0, 0, 0, 0 };
        try
        {
            await stream.WriteAsync(response, token).ConfigureAwait(false);
        }
        catch
        {
            // Client closed connection
        }
    }

    private static async Task<int> ReadExactAsync(Stream stream, byte[] buffer, int count, CancellationToken token)
    {
        int totalRead = 0;
        while (totalRead < count)
        {
            int read = await stream.ReadAsync(buffer.AsMemory(totalRead, count - totalRead), token).ConfigureAwait(false);
            if (read <= 0)
                break;
            totalRead += read;
        }
        return totalRead;
    }

    private NetworkAdapterInfo? SelectAdapter()
    {
        lock (_adapterLock)
        {
            if (_activeAdapters.Count == 0)
                return null;

            if (Strategy == ProxyBindingStrategy.LeastConnections)
            {
                // Find adapter with minimum open sessions
                return _activeAdapters
                    .OrderBy(a => _adapterSessions.TryGetValue(a.IpAddress, out int s) ? s : 0)
                    .FirstOrDefault() ?? _activeAdapters[0];
            }
            else
            {
                // Round-Robin
                int index = (int)((uint)Interlocked.Increment(ref _roundRobinIndex) % (uint)_activeAdapters.Count);
                return _activeAdapters[index];
            }
        }
    }

    private void Log(string message)
    {
        OnLog?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}
