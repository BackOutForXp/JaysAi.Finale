using System;
using System.Diagnostics;
using System.Linq;

namespace JaysAi.Finale.Security
{
    public static class StealthScanner
    {
        private static readonly string[] BlacklistedProcesses = new[]
        {
            "beservice",         // BattleEye
            "EasyAntiCheat",     // EAC
            "vgk",               // Vanguard (Valorant)
            "FaceItClient",      // FaceIt
            "Mhyprot2",          // Genshin anti-cheat
            "steamwebhelper",    // Potential flag
            "dwm",               // Desktop Window Manager (rare triggers)
        };

        public static bool IsSafeEnvironment()
        {
            var processes = Process.GetProcesses();
            return !processes.Any(p => BlacklistedProcesses.Contains(p.ProcessName, StringComparer.OrdinalIgnoreCase));
        }

        public static void PrintScanReport()
        {
            Console.WriteLine("[MONARCH] Running environment scan...");

            foreach (var process in Process.GetProcesses())
            {
                if (BlacklistedProcesses.Contains(process.ProcessName, StringComparer.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"⚠ Detected blacklisted process: {process.ProcessName}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("[MONARCH] Scan complete.");
        }
    }
}
