using System.Collections.Generic;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Integration
{
    public static class AutoDetectionHelper
    {
        private static Dictionary<string, OffsetProfile>? _cachedProfiles;

        public static void RunAutoDetection()
        {
            if (_cachedProfiles == null)
                _cachedProfiles = OffsetProfileLoader.LoadAllProfiles();

            GameDetector.AutoApplyOffsetProfile(_cachedProfiles);

            // Placeholder for future hardware/platform detection:
            DetectHardwareIntegration();
        }

        private static void DetectHardwareIntegration()
        {
            // TODO: Detect Cronus Zen, Titan Two, or Capture Card support
            // Example:
            // if (ZenHelper.IsConnected()) { ZenIntegration.Initialize(); }

            System.Console.WriteLine("[AutoDetection] Hardware detection not implemented yet.");
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Runs game detection and applies correct OffsetProfile
// ✅ Caches loaded OffsetProfiles to avoid reloading on every call
// ✅ Placeholder for controller/hardware integration detection
// - [ ] Implement Zen/Titan detection logic in DetectHardwareIntegration()
// - [ ] Tie into GUI checkbox "Auto Detect Mode"
// ===================================================================
