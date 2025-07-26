// neural v3.0
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JaysAi.Finale.SystemLogic.Logging;

namespace JaysAi.Finale.SystemLogic
{
    public static class UpdateChecker
    {
        private const string UpdateUrl = "https://yourserver.com/jaysai/version.json"; // replace with your real endpoint

        public static async Task<bool> IsUpdateAvailableAsync(string currentVersion)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(UpdateUrl);
                var updateInfo = JsonSerializer.Deserialize<UpdateInfo>(response);

                if (updateInfo is null || string.IsNullOrWhiteSpace(updateInfo.Version))
                    return false;

                return Version.TryParse(updateInfo.Version, out var serverVersion)
                    && Version.TryParse(currentVersion, out var localVersion)
                    && serverVersion > localVersion;
            }
            catch (Exception ex)
            {
                LogManager.Log($"[UpdateChecker] Failed to check for updates: {ex.Message}", LogLevel.Warning);
                return false;
            }
        }

        public static async Task<string?> GetLatestVersionAsync()
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(UpdateUrl);
                var updateInfo = JsonSerializer.Deserialize<UpdateInfo>(response);

                return updateInfo?.Version;
            }
            catch (Exception ex)
            {
                LogManager.Log($"[UpdateChecker] Could not retrieve latest version: {ex.Message}", LogLevel.Warning);
                return null;
            }
        }

        private class UpdateInfo
        {
            public string Version { get; set; } = string.Empty;
        }
    }
}
