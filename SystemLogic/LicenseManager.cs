// neural v3.0
using System;
using JaysAi.Finale.Security;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.SystemLogic
{
    public static class LicenseManager
    {
        private static string? _cachedLicenseKey;
        private static DateTime _lastValidationAttempt = DateTime.MinValue;
        private static bool _isLicenseValid = false;
        private const int ValidationCooldownSeconds = 10;

        public static bool IsLicenseValid
        {
            get
            {
                if ((DateTime.UtcNow - _lastValidationAttempt).TotalSeconds < ValidationCooldownSeconds)
                    return _isLicenseValid;

                _lastValidationAttempt = DateTime.UtcNow;
                _cachedLicenseKey = UserSettings.Current?.LicenseKey;

                if (string.IsNullOrWhiteSpace(_cachedLicenseKey))
                {
                    Logger.Warn("License key missing.");
                    _isLicenseValid = false;
                    return false;
                }

                _isLicenseValid = LicenseValidator.Validate(_cachedLicenseKey);
                Logger.Info($"License validation result: {_isLicenseValid}");
                return _isLicenseValid;
            }
        }

        public static void RefreshLicenseKey(string newKey)
        {
            if (!string.IsNullOrWhiteSpace(newKey))
            {
                _cachedLicenseKey = newKey;
                UserSettings.Current.LicenseKey = newKey;
                SettingsManager.Save();
                Logger.Info("License key updated and saved.");
            }
            else
            {
                Logger.Warn("Attempted to refresh with empty license key.");
            }
        }

        public static string? GetLicenseKey()
        {
            return _cachedLicenseKey ?? UserSettings.Current?.LicenseKey;
        }
    }
}
