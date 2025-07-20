// File: System\UpdateChecker.cs

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JaysAi.Finale.SystemLogic
{
    public static class UpdateChecker
    {
        private static readonly string VersionUrl = "https://yourdomain.com/jaysai/version.txt"; // Replace with real endpoint
        private static readonly string CurrentVersion = "1.0.0"; // This should be synced with AssemblyVersion in future

        public static async Task<bool> IsUpdateAvailableAsync()
        {
            try
            {
                using var httpClient = new HttpClient();
                string latestVersion = await httpClient.GetStringAsync(VersionUrl);

                return !string.IsNullOrWhiteSpace(latestVersion) &&
                       !latestVersion.Trim().Equals(CurrentVersion, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UpdateChecker] Failed to check for updates: {ex.Message}");
                return false;
            }
        }

        public static string GetCurrentVersion() => CurrentVersion;
    }
}
