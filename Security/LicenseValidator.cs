// File: Security/LicenseValidator.cs
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Security
{
    public static class LicenseValidator
    {
        private static readonly HttpClient _httpClient = new();

        /// <summary>
        /// Verifies the license key with the backend.
        /// </summary>
        public static async Task<bool> ValidateLicenseAsync(string licenseKey)
        {
            Logger.Log("Validating license key...");

            try
            {
                string hashedKey = HashLicenseKey(licenseKey);
                var response = await _httpClient.GetAsync($"https://your-license-server.com/check?key={hashedKey}");

                if (response.IsSuccessStatusCode)
                {
                    Logger.Log("License is valid.");
                    return true;
                }
                else
                {
                    Logger.Log("Invalid license.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"License check failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Hashes the license key using SHA256 before sending.
        /// </summary>
        public static string HashLicenseKey(string licenseKey)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(licenseKey);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// Allows offline fallback for testing or dev use.
        /// </summary>
        public static bool IsValidOfflineKey(string licenseKey)
        {
            return licenseKey == "DEV-KEY-1234";
        }
    }
}
