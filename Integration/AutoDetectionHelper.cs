//neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Integration
{
    public static class AutoDetectionHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder text, int count);

        private static readonly Dictionary<string, string> KnownProcesses = new()
        {
            { "ModernWarfare", "Call of Duty: Modern Warfare" },
            { "Warzone", "Call of Duty: Warzone" },
            { "BlackOps6", "Call of Duty: Black Ops 6" },
            { "FortniteClient", "Fortnite" },
            { "Apex", "Apex Legends" }
        };

        public static string? DetectActiveGame()
        {
            var windowTitle = GetActiveWindowTitle();
            if (string.IsNullOrWhiteSpace(windowTitle)) return null;

            foreach (var kvp in KnownProcesses)
            {
                if (windowTitle.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                    return kvp.Value;
            }

            return null;
        }

        public static bool IsGameRunning(string gameTitle)
        {
            var activeTitle = GetActiveWindowTitle();
            return !string.IsNullOrEmpty(activeTitle) &&
                   activeTitle.Contains(gameTitle, StringComparison.OrdinalIgnoreCase);
        }

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            var buffer = new System.Text.StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            return (GetWindowText(handle, buffer, nChars) > 0)
                ? buffer.ToString()
                : string.Empty;
        }
    }
}
