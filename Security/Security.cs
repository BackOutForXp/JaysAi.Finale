using System;
using JaysAi.Finale.System;

namespace JaysAi.Finale.Security
{
    public static class LicenseValidator
    {
        public static void Validate()
        {
            string key = ConfigManager.Config.LastLicenseKey.Trim();

            switch (key.ToUpperInvariant())
            {
                case "PUB-FREE":
                    FeatureManager.CurrentTier = LicenseTier.Public;
                    break;

                case "LITE-1234":
                    FeatureManager.CurrentTier = LicenseTier.Lite;
                    break;

                case "ELITE-7777":
                    FeatureManager.CurrentTier = LicenseTier.Elite;
                    break;

                case "OWNR-DEV":
                case "OWNER-9999":
                    FeatureManager.CurrentTier = LicenseTier.Owner;
                    break;

                default:
                    FeatureManager.CurrentTier = LicenseTier.Public;
                    Console.WriteLine($"[LicenseValidator] Unknown key '{key}' – defaulting to Public tier.");
                    break;
            }

            Console.WriteLine($"[LicenseValidator] Tier: {FeatureManager.CurrentTier}");
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ To finalize this module:
// - [x] Reads license from ConfigManager.Config.LastLicenseKey
// - [x] Assigns license tier to FeatureManager.CurrentTier
// - [ ] Add cloud API license validation later (via HTTPS POST)
// - [ ] Hide Owner-only UI features based on this tier
// - [ ] Inject Validate() into App.xaml.cs → during app startup
// ===================================================================
