using System.Collections.Generic;

namespace JaysAi.Finale.SystemLogic
{
    public static class LicenseValidator
    {
        public static string CurrentTier { get; private set; } = "Public";

        private static readonly Dictionary<string, string> LicenseTiers = new()
        {
            { "FREE-ESP-1234", "Public" },
            { "ELITE-KEY-5678", "Elite" },
            { "OWNER-MODE-9999", "Owner" }
        };

        public static bool ValidateKey(string key)
        {
            if (LicenseTiers.TryGetValue(key.Trim(), out string tier))
            {
                CurrentTier = tier;
                SystemConfig.LastLicenseKey = key.Trim();
                return true;
            }

            return false;
        }

        // Optional reuse after restart
        public static bool Validate()
        {
            string? cached = SystemConfig.LastLicenseKey;
            return !string.IsNullOrEmpty(cached) && ValidateKey(cached);
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ 3-tier system with offline test keys
// ✅ Cache-friendly reuse via ConfigManager
// ✅ All tier logic routed through FeatureManager
// - [ ] Future: Replace with API call to online key database
// - [ ] Future: Add hardware ID bind or IP check
// ===================================================================
