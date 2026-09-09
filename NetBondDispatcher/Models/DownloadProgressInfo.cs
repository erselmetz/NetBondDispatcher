using System.Net;

namespace NetBondDispatcher.Models;

public class DownloadProgressInfo
{
    public int ChunkIndex { get; set; }
    public IPAddress? AdapterIp { get; set; }
    public string AdapterName { get; set; } = string.Empty;
    public long BytesDownloaded { get; set; }
    public long TotalChunkBytes { get; set; }
    public double BytesPerSecond { get; set; }
    public bool IsCompleted { get; set; }
    public string? ErrorMessage { get; set; }

    public double SpeedMbps => (BytesPerSecond * 8.0) / (1024.0 * 1024.0);
    public double SpeedMBps => BytesPerSecond / (1024.0 * 1024.0);
}
