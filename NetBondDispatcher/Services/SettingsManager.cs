using System.Text.Json;
using NetBondDispatcher.Models;

namespace NetBondDispatcher.Services;

public class SettingsManager
{
    private static readonly string ConfigDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "NetBondDispatcher");

    private static readonly string ConfigFilePath = Path.Combine(ConfigDirectory, "config.json");

    private readonly StartupManager _startupManager = new();

    public AppConfig LoadSettings()
    {
        try
        {
            if (File.Exists(ConfigFilePath))
            {
                var json = File.ReadAllText(ConfigFilePath);
                var config = JsonSerializer.Deserialize<AppConfig>(json);
                if (config != null)
                {
                    // Also verify registry state
                    config.StartWithWindows = _startupManager.IsRunOnStartupEnabled();
                    return config;
                }
            }
        }
        catch
        {
            // Fallback to defaults on read error
        }

        var defaultConfig = new AppConfig
        {
            StartWithWindows = _startupManager.IsRunOnStartupEnabled()
        };
        return defaultConfig;
    }

    public bool SaveSettings(AppConfig config)
    {
        try
        {
            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }

            var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFilePath, json);

            // Sync startup with registry
            _startupManager.SetRunOnStartup(config.StartWithWindows);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
