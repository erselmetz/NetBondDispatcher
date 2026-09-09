using Velopack;
using Velopack.Sources;

namespace NetBondDispatcher.Services;

public class UpdateService
{
    private readonly string _defaultRepoUrl = "https://github.com/erselmetz/NetBondDispatcher";

    public async Task<(bool hasUpdate, string? newVersion, string message)> CheckForUpdatesAsync(string? repoUrl = null)
    {
        var targetRepo = string.IsNullOrWhiteSpace(repoUrl) ? _defaultRepoUrl : repoUrl.Trim();

        try
        {
            var source = new GithubSource(targetRepo, accessToken: null, prerelease: false);
            var updateManager = new UpdateManager(source);

            if (!updateManager.IsInstalled)
            {
                return (false, null, "App is running in unpackaged debug mode. Velopack updates require a packaged install.");
            }

            var updateInfo = await updateManager.CheckForUpdatesAsync();
            if (updateInfo == null)
            {
                return (false, null, "NetBondDispatcher is up to date.");
            }

            return (true, updateInfo.TargetFullRelease?.Version.ToFullString(), $"Update {updateInfo.TargetFullRelease?.Version} available!");
        }
        catch (Exception ex)
        {
            return (false, null, $"Update check failed: {ex.Message}");
        }
    }

    public async Task<bool> DownloadAndApplyUpdateAsync(string? repoUrl = null, Action<int>? progressCallback = null)
    {
        var targetRepo = string.IsNullOrWhiteSpace(repoUrl) ? _defaultRepoUrl : repoUrl.Trim();

        try
        {
            var source = new GithubSource(targetRepo, accessToken: null, prerelease: false);
            var updateManager = new UpdateManager(source);

            if (!updateManager.IsInstalled)
                return false;

            var updateInfo = await updateManager.CheckForUpdatesAsync();
            if (updateInfo == null)
                return false;

            await updateManager.DownloadUpdatesAsync(updateInfo, progressCallback);
            updateManager.ApplyUpdatesAndRestart(updateInfo);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
