using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Integration
{
    public static class GameDetector
    {
        private static Dictionary<string, string> GameProcessMap = new()
        {
            // Maps process name to OffsetProfile key
            { "bo6", "BO6" },
            { "r5apex", "Apex" }
        };

        public static string? DetectRunningGame()
        {
            var processes = Process.GetProcesses();

            foreach (var proc in processes)
            {
                if (GameProcessMap.TryGetValue(proc.ProcessName.ToLower(), out var profileKey))
                {
                    Console.WriteLine($"[GameDetector] Detected game: {profileKey}");
                    return profileKey;
                }
            }

            Console.WriteLine("[GameDetector] No supported game detected.");
            return null;
        }

        public static void AutoApplyOffsetProfile(Dictionary<string, OffsetProfile> allProfiles)
        {
            var detected = DetectRunningGame();
            if (detected != null && allProfiles.ContainsKey(detected))
            {
                allProfiles[detected].Apply();
                Console.WriteLine($"[GameDetector] Auto-applied offset profile for: {detected}");
            }
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Scans running processes for known games (bo6, r5apex, etc.)
// ✅ Matches to OffsetProfile via OffsetProfileLoader
// ✅ Applies offsets to OffsetMap on detection
// - [ ] Link to GUI toggle for "Auto-detect Game"
// - [ ] Add fallback dialog if game not found
// ===================================================================
