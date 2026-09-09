using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using NetBondDispatcher.Models;

namespace NetBondDispatcher.Services;

public class NetworkMonitor
{
    private readonly List<NetworkAdapterInfo> _adapters = new();
    private readonly object _lock = new();

    public event Action<List<NetworkAdapterInfo>, double, double>? OnMetricsUpdated; // adapters, totalDownBps, totalUpBps

    public List<NetworkAdapterInfo> GetAdapters()
    {
        lock (_lock)
        {
            return _adapters.ToList();
        }
    }

    public List<NetworkAdapterInfo> GetActiveBondedAdapters()
    {
        lock (_lock)
        {
            return _adapters.Where(a => a.IsIncludedInPool && a.Status == OperationalStatus.Up).ToList();
        }
    }

    public void ScanAdapters()
    {
        var discovered = new List<NetworkAdapterInfo>();
        var interfaces = NetworkInterface.GetAllNetworkInterfaces();

        foreach (var ni in interfaces)
        {
            if (ni.OperationalStatus != OperationalStatus.Up)
                continue;

            if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback ||
                ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                continue;

            var ipProps = ni.GetIPProperties();
            foreach (var addr in ipProps.UnicastAddresses)
            {
                if (addr.Address.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(addr.Address))
                {
                    discovered.Add(new NetworkAdapterInfo
                    {
                        Id = ni.Id,
                        Name = ni.Name,
                        Description = ni.Description,
                        IpAddress = addr.Address,
                        InterfaceType = ni.NetworkInterfaceType,
                        Status = ni.OperationalStatus,
                        Speed = ni.Speed,
                        IsIncludedInPool = true
                    });
                }
            }
        }

        lock (_lock)
        {
            // Preserve previous byte counters and selection state if adapter already exists
            foreach (var newAdapter in discovered)
            {
                var existing = _adapters.FirstOrDefault(a => a.Id == newAdapter.Id && a.IpAddress.Equals(newAdapter.IpAddress));
                if (existing != null)
                {
                    newAdapter.IsIncludedInPool = existing.IsIncludedInPool;
                    newAdapter.PreviousBytesReceived = existing.PreviousBytesReceived;
                    newAdapter.PreviousBytesSent = existing.PreviousBytesSent;
                    newAdapter.LastSampleTime = existing.LastSampleTime;
                    newAdapter.ActiveSessionCount = existing.ActiveSessionCount;
                }
            }

            _adapters.Clear();
            _adapters.AddRange(discovered.OrderBy(a => a.Name));
        }
    }

    /// <summary>
    /// Computes delta speed (bytes/sec) for each adapter using NetworkInterface.GetIPv4Statistics()
    /// </summary>
    public void TickSpeedCalculation()
    {
        var interfaces = NetworkInterface.GetAllNetworkInterfaces().ToDictionary(ni => ni.Id, ni => ni);
        var now = DateTime.UtcNow;
        double aggregateDownBps = 0;
        double aggregateUpBps = 0;

        lock (_lock)
        {
            foreach (var adapter in _adapters)
            {
                if (!interfaces.TryGetValue(adapter.Id, out var ni))
                    continue;

                try
                {
                    var stats = ni.GetIPv4Statistics();
                    long currentRx = stats.BytesReceived;
                    long currentTx = stats.BytesSent;

                    if (adapter.PreviousBytesReceived >= 0 && adapter.LastSampleTime != DateTime.MinValue)
                    {
                        double deltaSeconds = (now - adapter.LastSampleTime).TotalSeconds;
                        if (deltaSeconds > 0)
                        {
                            long deltaRx = Math.Max(0, currentRx - adapter.PreviousBytesReceived);
                            long deltaTx = Math.Max(0, currentTx - adapter.PreviousBytesSent);

                            adapter.DownloadSpeedBytesPerSec = deltaRx / deltaSeconds;
                            adapter.UploadSpeedBytesPerSec = deltaTx / deltaSeconds;

                            if (adapter.IsIncludedInPool)
                            {
                                aggregateDownBps += adapter.DownloadSpeedBytesPerSec;
                                aggregateUpBps += adapter.UploadSpeedBytesPerSec;
                            }
                        }
                    }

                    adapter.PreviousBytesReceived = currentRx;
                    adapter.PreviousBytesSent = currentTx;
                    adapter.LastSampleTime = now;
                }
                catch
                {
                    // Adapter might have disconnected or stats unavailable
                }
            }
        }

        OnMetricsUpdated?.Invoke(GetAdapters(), aggregateDownBps, aggregateUpBps);
    }

    public void SetAdapterIncluded(string adapterId, string ipAddress, bool isIncluded)
    {
        lock (_lock)
        {
            var target = _adapters.FirstOrDefault(a => a.Id == adapterId && a.IpAddress.ToString() == ipAddress);
            if (target != null)
            {
                target.IsIncludedInPool = isIncluded;
            }
        }
    }

    public void UpdateActiveSessions(Dictionary<IPAddress, int> sessionCounts)
    {
        lock (_lock)
        {
            foreach (var adapter in _adapters)
            {
                adapter.ActiveSessionCount = sessionCounts.TryGetValue(adapter.IpAddress, out int count) ? count : 0;
            }
        }
    }
}
