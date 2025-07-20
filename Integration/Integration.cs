using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Integration
{
    public static class AutoIntegrationManager
    {
        public static void RunFullScan()
        {
            System.Console.WriteLine("[Integration] Running full system scan...");

            // 1. Load all offset profiles
            var profiles = OffsetProfileLoader.LoadAllProfiles();

            // 2. Detect game and apply matching profile
            GameDetector.AutoApplyOffsetProfile(profiles);

            // 3. Check for Zen and Titan hardware
            if (ZenHelper.IsConnected())
                ZenHelper.Initialize();

            if (TitanHelper.IsConnected())
                TitanHelper.Initialize();

            // 4. Check for capture cards
            var captureCards = CaptureCardHelper.GetConnectedCaptureDevices();
            if (captureCards.Count > 0)
            {
                System.Console.WriteLine($"[Integration] Detected capture devices: {string.Join(", ", captureCards)}");
            }
            else
            {
                System.Console.WriteLine("[Integration] No capture card found.");
            }

            System.Console.WriteLine("[Integration] System scan complete.");
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Ties together all integration helpers into one unified call
// ✅ Scans game, hardware, and capture devices
// ✅ Ready for GUI binding and Auto Mode toggle
// - [ ] Add async/parallel logic for faster scans
// - [ ] Link to GUI auto-scan checkbox or launch sequence
// - [ ] Allow user to disable specific scan types (e.g. "Skip Zen")
// ===================================================================
