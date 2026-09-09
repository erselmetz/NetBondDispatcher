namespace NetBondDispatcher.Models;

public class AppConfig
{
    public bool StartWithWindows { get; set; } = false;
    public bool StartMinimizedToTray { get; set; } = false;
    public bool MinimizeOnClose { get; set; } = true;
    public bool AutoStartProxy { get; set; } = false;
    public int ProxyPort { get; set; } = 10808;
    public string ProxyStrategy { get; set; } = "RoundRobin"; // "RoundRobin" or "LeastConnections"
    public int StreamsPerAdapter { get; set; } = 2;
    public string GitHubRepoUrl { get; set; } = "https://github.com/erselmetz/NetBondDispatcher";
}
