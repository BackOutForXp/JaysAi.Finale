//neural v3.0
using System;
using System.Diagnostics;
using System.Linq;

namespace JaysAi.Finale.Integration
{
    public static class GameDetector
    {
        private static readonly string[] TargetProcesses = new[]
        {
            "cod",            // Call of Duty (generic)
            "bo6",            // Black Ops 6
            "warzone",        // Warzone
            "iw8",            // Modern Warfare engine internal
            "vanguard",       // Vanguard
            "t6sp",           // BO2 SP (legacy support)
        };

        public static string? ActiveGameName => DetectGame();

        public static bool IsSupportedGameRunning()
        {
            return DetectGame() != null;
        }

        private static string? DetectGame()
        {
            foreach (var proc in Process.GetProcesses())
            {
                try
                {
                    if (TargetProcesses.Any(keyword => proc.ProcessName.ToLower().Contains(keyword)))
                        return proc.ProcessName;
                }
                catch
                {
                    // Ignore inaccessible processes
                }
            }

            return null;
        }

        public static void LogDetectedGame()
        {
            var game = DetectGame();
            if (game != null)
                Console.WriteLine($"[GameDetector] Detected running game: {game}");
            else
                Console.WriteLine("[GameDetector] No supported game detected.");
        }
    }
}
