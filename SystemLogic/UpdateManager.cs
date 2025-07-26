// neural v3.0
using System;
using System.Threading.Tasks;
using JaysAi.Finale.SystemLogic.Logging;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class UpdateManager
    {
        private static readonly Lazy<UpdateManager> _instance = new(() => new UpdateManager());
        public static UpdateManager Instance => _instance.Value;

        private string _currentVersion = "1.0.0"; // TODO: Set from BuildInfo or config at runtime

        private UpdateManager() { }

        public async Task CheckForUpdatesAsync()
        {
            LogManager.Log("[UpdateManager] Checking for updates...", LogLevel.Info);

            bool updateAvailable = await UpdateChecker.IsUpdateAvailableAsync(_currentVersion);
            if (updateAvailable)
            {
                var latest = await UpdateChecker.GetLatestVersionAsync();
                LogManager.Log($"[UpdateManager] New version available: {latest}", LogLevel.Success);
                OnUpdateAvailable?.Invoke(this, latest ?? "Unknown");
            }
            else
            {
                LogManager.Log("[UpdateManager] You are on the latest version.", LogLevel.Info);
            }
        }

        public void SetCurrentVersion(string version)
        {
            _currentVersion = version;
            LogManager.Log($"[UpdateManager] Current version set to {version}", LogLevel.Debug);
        }

        public event EventHandler<string>? OnUpdateAvailable;
    }
}
