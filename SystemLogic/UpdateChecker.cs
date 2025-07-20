//monarch v2.1 – Auto-update logic enabled
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JaysAi.Finale.SystemLogic
{
    public class UpdateChecker
    {
        private static readonly string updateUrl = "https://yourapi.com/jaysai/version"; // Replace with actual update endpoint

        public static async Task<bool> IsUpdateAvailableAsync(string currentVersion)
        {
            try
            {
                using HttpClient client = new HttpClient();
                string latestVersion = await client.GetStringAsync(updateUrl);
                return !string.IsNullOrEmpty(latestVersion) && !latestVersion.Equals(currentVersion, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                // Log the error internally (optional)
                Console.WriteLine($"Update check failed: {ex.Message}");
                return false;
            }
        }
    }
}
