// neural v3.0
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Utility;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JaysAi.Finale.Security
{
    public sealed class LicenseValidator
    {
        private static readonly Lazy<LicenseValidator> _instance = new(() => new LicenseValidator());
        public static LicenseValidator Instance => _instance.Value;

        private const string ValidationUrl = "https://api.jaysai.net/license/validate"; // Placeholder
        private const string ProductId = "JAYS-AI-MONARCH"; // For multi-product loaders

        private string _cachedLicenseKey = string.Empty;
        private bool _isValidated = false;

        public bool IsLicenseValid => _isValidated;

        private LicenseValidator() { }

        public async Task<bool> ValidateAsync(string licenseKey, string hwid)
        {
            try
            {
                using var client = new HttpClient();
                var payload = new StringContent(
                    $"key={licenseKey}&hwid={hwid}&product={ProductId}", Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(ValidationUrl, payload);
                var responseText = await response.Content.ReadAsStringAsync();

                _isValidated = response.IsSuccessStatusCode && responseText.Contains("valid", StringComparison.OrdinalIgnoreCase);
                _cachedLicenseKey = _isValidated ? licenseKey : string.Empty;

                return _isValidated;
            }
            catch (Exception ex)
            {
                Logger.Log("License validation failed: " + ex.Message);
                _isValidated = false;
                return false;
            }
        }

        public string GenerateHWID()
        {
            using var sha256 = SHA256.Create();
            var rawData = $"{Environment.MachineName}-{Environment.UserName}-{Environment.OSVersion}";
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public void Invalidate()
        {
            _isValidated = false;
            _cachedLicenseKey = string.Empty;
        }

        public string GetCachedLicense() => _cachedLicenseKey;
    }
}
