using System.Net;
using System.Net.NetworkInformation;

namespace NetBondDispatcher.Models;

public class NetworkAdapterInfo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IPAddress IpAddress { get; set; } = IPAddress.None;
    public NetworkInterfaceType InterfaceType { get; set; }
    public OperationalStatus Status { get; set; }
    public long Speed { get; set; }
    public bool IsIncludedInPool { get; set; } = true;

    // Real-time speed calculation metrics
    public long PreviousBytesReceived { get; set; } = -1;
    public long PreviousBytesSent { get; set; } = -1;
    public DateTime LastSampleTime { get; set; } = DateTime.MinValue;
    public double DownloadSpeedBytesPerSec { get; set; }
    public double UploadSpeedBytesPerSec { get; set; }

    // Active connection tracking
    public int ActiveSessionCount { get; set; }

    public string DisplayName => $"[{IpAddress}] {Name} ({InterfaceType} - {Description})";

    public string FormattedDownloadSpeed => FormatBytesPerSec(DownloadSpeedBytesPerSec);
    public string FormattedUploadSpeed => FormatBytesPerSec(UploadSpeedBytesPerSec);

    public static string FormatBytesPerSec(double bytesPerSec)
    {
        if (bytesPerSec < 1024)
            return $"{bytesPerSec:F0} B/s";
        if (bytesPerSec < 1024 * 1024)
            return $"{bytesPerSec / 1024.0:F1} KB/s";
        if (bytesPerSec < 1024 * 1024 * 1024)
            return $"{bytesPerSec / (1024.0 * 1024.0):F2} MB/s";
        return $"{bytesPerSec / (1024.0 * 1024.0 * 1024.0):F2} GB/s";
    }

    public override string ToString() => DisplayName;
}
