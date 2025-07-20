using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace JaysAi.Finale.SystemLogic
{
    public static class TargetLogger
    {
        private static readonly List<string> _log = new();
        private static readonly string _logPath = "Logs/aimlog.txt";

        public static void LogTargetLock(Vector2 targetPos, float distance, string sourceModule)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] Locked by {sourceModule} → X:{targetPos.X:F1} Y:{targetPos.Y:F1} | Dist:{distance:F1}";
            _log.Add(entry);
            Console.WriteLine(entry);
        }

        public static void LogMiss(string module, string reason)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] MISS by {module} → Reason: {reason}";
            _log.Add(entry);
            Console.WriteLine(entry);
        }

        public static void LogDebug(string message)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] DEBUG: {message}";
            _log.Add(entry);
        }

        public static void Save()
        {
            try
            {
                Directory.CreateDirectory("Logs");
                File.WriteAllLines(_logPath, _log);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Logger] Failed to save log: {ex.Message}");
            }
        }

        public static void Clear()
        {
            _log.Clear();
            if (File.Exists(_logPath))
                File.Delete(_logPath);
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ To finalize this module:
// - [x] Used by: AimAssist, TriggerBot, SilentAim, etc.
// - [ ] Add config toggle to enable/disable logging
// - [ ] Save stats into tier folder or profile-based directories
// - [ ] Optional: Train MonarchAimAI using replay logs
// ===================================================================
