using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using NetBondDispatcher.Models;

namespace NetBondDispatcher.Services;

public class ChunkDownloader
{
    private const string BrowserUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";
    private CancellationTokenSource? _cts;
    public bool IsDownloading => _cts != null && !_cts.IsCancellationRequested;

    public event Action<DownloadProgressInfo>? OnChunkProgress;
    public event Action<long, long, double>? OnOverallProgress; // downloadedBytes, totalBytes, speedMBps
    public event Action<string>? OnLog;
    public event Action<bool, string?>? OnCompleted; // success, error message

    public async Task StartDownloadAsync(
        string url,
        string destinationFilePath,
        List<NetworkAdapterInfo> adapters,
        int requestedChunkCount = 4)
    {
        if (IsDownloading)
            return;

        if (adapters.Count == 0)
        {
            OnCompleted?.Invoke(false, "No active network adapters selected in the bonding pool.");
            return;
        }

        // 1. Resolve destination file path & auto-name if needed
        string resolvedDest = ResolveDestinationPath(url, destinationFilePath);

        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            Log($"[Downloader] Starting download for: {url}");
            Log($"[Downloader] Target file destination: {resolvedDest}");

            // 2. Probe file metadata (Content-Length and HTTP Range support)
            var (contentLength, supportsRange) = await ProbeUrlAsync(url, adapters[0].IpAddress, token);

            if (contentLength <= 0)
            {
                Log("[Downloader] Could not determine file size via HEAD/Range probes. Downloading as single-stream...");
                await DownloadSingleStreamAsync(url, resolvedDest, adapters[0], token);
                OnCompleted?.Invoke(true, null);
                return;
            }

            Log($"[Downloader] Detected size: {contentLength:N0} bytes ({contentLength / (1024.0 * 1024.0):F2} MB). Range support: {supportsRange}");

            if (!supportsRange)
            {
                Log("[Downloader] Server does not support Range requests. Downloading via primary adapter stream...");
                await DownloadSingleStreamAsync(url, resolvedDest, adapters[0], token);
                OnCompleted?.Invoke(true, null);
                return;
            }

            // 3. Pre-allocate destination file
            var directory = Path.GetDirectoryName(resolvedDest);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fsInit = new FileStream(resolvedDest, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                fsInit.SetLength(contentLength);
            }

            // 4. Plan chunk ranges auto-balanced across active adapters
            int totalChunks = Math.Max(1, requestedChunkCount);
            long chunkSize = contentLength / totalChunks;
            Log($"[Downloader] Auto-balancing {totalChunks} chunks across {adapters.Count} active adapter(s)...");

            long totalDownloaded = 0;
            var swOverall = Stopwatch.StartNew();
            var chunkTasks = new List<Task>();

            for (int i = 0; i < totalChunks; i++)
            {
                int chunkIndex = i;
                long startByte = chunkIndex * chunkSize;
                long endByte = (chunkIndex == totalChunks - 1) ? contentLength - 1 : (startByte + chunkSize - 1);
                var assignedAdapter = adapters[chunkIndex % adapters.Count];

                chunkTasks.Add(Task.Run(async () =>
                {
                    await DownloadChunkAsync(
                        url,
                        resolvedDest,
                        chunkIndex,
                        startByte,
                        endByte,
                        assignedAdapter,
                        progress =>
                        {
                            Interlocked.Add(ref totalDownloaded, progress.BytesDownloaded);
                            double elapsedSec = swOverall.Elapsed.TotalSeconds;
                            double overallSpeed = elapsedSec > 0 ? (totalDownloaded / (1024.0 * 1024.0)) / elapsedSec : 0;
                            OnOverallProgress?.Invoke(totalDownloaded, contentLength, overallSpeed);
                            OnChunkProgress?.Invoke(progress);
                        },
                        token);
                }, token));
            }

            await Task.WhenAll(chunkTasks).ConfigureAwait(false);

            swOverall.Stop();
            double finalSpeed = swOverall.Elapsed.TotalSeconds > 0
                ? (contentLength / (1024.0 * 1024.0)) / swOverall.Elapsed.TotalSeconds
                : 0;

            Log($"[Downloader] Finished successfully in {swOverall.Elapsed.TotalSeconds:F1}s (Average: {finalSpeed:F2} MB/s).");
            OnCompleted?.Invoke(true, null);
        }
        catch (OperationCanceledException)
        {
            Log("[Downloader] Download canceled by user.");
            OnCompleted?.Invoke(false, "Download canceled.");
        }
        catch (Exception ex)
        {
            Log($"[Downloader Error] {ex.Message}");
            OnCompleted?.Invoke(false, ex.Message);
        }
        finally
        {
            _cts?.Dispose();
            _cts = null;
        }
    }

    public void Cancel()
    {
        if (IsDownloading)
        {
            Log("[Downloader] Canceling active download...");
            _cts?.Cancel();
        }
    }

    private static string ResolveDestinationPath(string url, string destinationPath)
    {
        string savePath = string.IsNullOrWhiteSpace(destinationPath)
            ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")
            : destinationPath.Trim();

        try
        {
            string fileName = Path.GetFileName(new Uri(url).LocalPath);
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "downloaded_file.bin";

            if (Directory.Exists(savePath) || !Path.HasExtension(savePath) || savePath.EndsWith("\\") || savePath.EndsWith("/"))
            {
                savePath = Path.Combine(savePath, fileName);
            }
        }
        catch
        {
            if (Directory.Exists(savePath))
                savePath = Path.Combine(savePath, "downloaded_file.bin");
        }

        return savePath;
    }

    private async Task<(long contentLength, bool supportsRange)> ProbeUrlAsync(string url, IPAddress bindIp, CancellationToken token)
    {
        using var client = CreateHttpClientForAdapter(bindIp);

        // 1. Try HEAD request first
        try
        {
            using var headReq = new HttpRequestMessage(HttpMethod.Head, url);
            using var headResp = await client.SendAsync(headReq, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

            if (headResp.IsSuccessStatusCode)
            {
                long length = headResp.Content.Headers.ContentLength ?? -1;
                bool acceptRanges = headResp.Headers.AcceptRanges.Contains("bytes") ||
                                    (headResp.Content.Headers.TryGetValues("Accept-Ranges", out var values) && values.Contains("bytes"));
                if (length > 0)
                {
                    Log($"[Downloader Probe] HEAD request succeeded. Length: {length:N0} bytes, Accept-Ranges: {acceptRanges}");
                    return (length, acceptRanges || length > 0);
                }
            }
            else
            {
                Log($"[Downloader Probe] HEAD request returned HTTP {(int)headResp.StatusCode} {headResp.ReasonPhrase}. Falling back to Range probe.");
            }
        }
        catch (Exception ex)
        {
            Log($"[Downloader Probe] HEAD probe exception: {ex.Message}. Falling back to Range probe.");
        }

        // 2. Fallback: Try GET with Range bytes=0-0 to test range support and inspect Content-Range
        try
        {
            using var rangeReq = new HttpRequestMessage(HttpMethod.Get, url);
            rangeReq.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(0, 0);

            using var rangeResp = await client.SendAsync(rangeReq, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

            if (rangeResp.StatusCode == HttpStatusCode.PartialContent)
            {
                long fullLength = rangeResp.Content.Headers.ContentRange?.Length ?? -1;
                Log($"[Downloader Probe] Range probe (bytes=0-0) succeeded with 206 Partial Content. Total length: {fullLength:N0} bytes.");
                return (fullLength, true);
            }

            if (rangeResp.IsSuccessStatusCode)
            {
                // Server returned 200 OK (ignored Range header)
                long length = rangeResp.Content.Headers.ContentLength ?? -1;
                Log($"[Downloader Probe] Range probe returned 200 OK without partial content support. Total length: {length:N0} bytes.");
                return (length, false);
            }

            string errorSnippet = string.Empty;
            try
            {
                errorSnippet = await rangeResp.Content.ReadAsStringAsync(token).ConfigureAwait(false);
                if (errorSnippet.Length > 200) errorSnippet = errorSnippet.Substring(0, 200) + "...";
            }
            catch { }

            Log($"[Downloader Probe] GET Range probe returned HTTP {(int)rangeResp.StatusCode} {rangeResp.ReasonPhrase}: {errorSnippet}");
        }
        catch (Exception ex)
        {
            Log($"[Downloader Probe] GET Range probe failed: {ex.Message}");
        }

        return (-1, false);
    }

    private async Task DownloadChunkAsync(
        string url,
        string destinationFilePath,
        int chunkIndex,
        long startByte,
        long endByte,
        NetworkAdapterInfo adapter,
        Action<DownloadProgressInfo> progressCallback,
        CancellationToken token)
    {
        using var client = CreateHttpClientForAdapter(adapter.IpAddress);
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(startByte, endByte);

        long chunkTotal = endByte - startByte + 1;
        long chunkDownloaded = 0;
        var sw = Stopwatch.StartNew();
        long lastReportedBytes = 0;
        long lastReportTime = 0;

        using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            string errorBody = string.Empty;
            try
            {
                errorBody = await response.Content.ReadAsStringAsync(token).ConfigureAwait(false);
                if (errorBody.Length > 200) errorBody = errorBody.Substring(0, 200) + "...";
            }
            catch { }

            string errorDetail = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {errorBody}";
            Log($"[Chunk #{chunkIndex + 1} Error] ({adapter.Name} - {adapter.IpAddress}) {errorDetail}");

            progressCallback(new DownloadProgressInfo
            {
                ChunkIndex = chunkIndex,
                AdapterIp = adapter.IpAddress,
                AdapterName = adapter.Name,
                BytesDownloaded = 0,
                TotalChunkBytes = chunkTotal,
                BytesPerSecond = 0,
                IsCompleted = false,
                ErrorMessage = errorDetail
            });

            throw new HttpRequestException($"Chunk #{chunkIndex + 1} failed with {errorDetail}", null, response.StatusCode);
        }

        using var responseStream = await response.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
        using var fileStream = new FileStream(destinationFilePath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
        fileStream.Seek(startByte, SeekOrigin.Begin);

        byte[] buffer = new byte[65536];
        int bytesRead;

        while ((bytesRead = await responseStream.ReadAsync(buffer.AsMemory(0, buffer.Length), token).ConfigureAwait(false)) > 0)
        {
            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), token).ConfigureAwait(false);
            chunkDownloaded += bytesRead;

            long elapsedMs = sw.ElapsedMilliseconds;
            if (elapsedMs - lastReportTime >= 250 || chunkDownloaded == chunkTotal)
            {
                long deltaBytes = chunkDownloaded - lastReportedBytes;
                double deltaSec = Math.Max(0.001, (elapsedMs - lastReportTime) / 1000.0);
                double bps = deltaBytes / deltaSec;

                progressCallback(new DownloadProgressInfo
                {
                    ChunkIndex = chunkIndex,
                    AdapterIp = adapter.IpAddress,
                    AdapterName = adapter.Name,
                    BytesDownloaded = deltaBytes,
                    TotalChunkBytes = chunkTotal,
                    BytesPerSecond = bps,
                    IsCompleted = chunkDownloaded >= chunkTotal
                });

                lastReportedBytes = chunkDownloaded;
                lastReportTime = elapsedMs;
            }
        }
    }

    private async Task DownloadSingleStreamAsync(
        string url,
        string destinationFilePath,
        NetworkAdapterInfo adapter,
        CancellationToken token)
    {
        using var client = CreateHttpClientForAdapter(adapter.IpAddress);
        using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            string errorBody = string.Empty;
            try
            {
                errorBody = await response.Content.ReadAsStringAsync(token).ConfigureAwait(false);
                if (errorBody.Length > 200) errorBody = errorBody.Substring(0, 200) + "...";
            }
            catch { }

            string errorDetail = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {errorBody}";
            Log($"[SingleStream Error] {errorDetail}");
            throw new HttpRequestException($"Download failed with {errorDetail}", null, response.StatusCode);
        }

        long? totalBytes = response.Content.Headers.ContentLength;
        using var stream = await response.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
        using var fs = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None);

        byte[] buffer = new byte[65536];
        int read;
        long downloaded = 0;
        var sw = Stopwatch.StartNew();

        while ((read = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), token).ConfigureAwait(false)) > 0)
        {
            await fs.WriteAsync(buffer.AsMemory(0, read), token).ConfigureAwait(false);
            downloaded += read;

            double elapsedSec = sw.Elapsed.TotalSeconds;
            double speedMBps = elapsedSec > 0 ? (downloaded / (1024.0 * 1024.0)) / elapsedSec : 0;
            OnOverallProgress?.Invoke(downloaded, totalBytes ?? downloaded, speedMBps);
        }
    }

    private static HttpClient CreateHttpClientForAdapter(IPAddress adapterIp)
    {
        var handler = new SocketsHttpHandler
        {
            ConnectCallback = async (context, cancellationToken) =>
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    NoDelay = true
                };

                try
                {
                    socket.Bind(new IPEndPoint(adapterIp, 0));
                }
                catch
                {
                    // Fallback to default route if adapter binding fails
                }

                await socket.ConnectAsync(context.DnsEndPoint, cancellationToken).ConfigureAwait(false);
                return new NetworkStream(socket, ownsSocket: true);
            },
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            AutomaticDecompression = DecompressionMethods.All
        };

        var client = new HttpClient(handler, disposeHandler: true)
        {
            Timeout = TimeSpan.FromMinutes(10)
        };

        // Add standard browser headers to prevent 403 Forbidden on CDNs & servers
        client.DefaultRequestHeaders.UserAgent.ParseAdd(BrowserUserAgent);
        client.DefaultRequestHeaders.Accept.ParseAdd("*/*");

        return client;
    }

    private void Log(string message)
    {
        OnLog?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}
