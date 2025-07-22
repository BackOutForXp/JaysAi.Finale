//heavenly v3.0
using System;
using System.Collections.Generic;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.AI
{
    public static class RuntimeBehaviorLog
    {
        private static readonly List<string> LogEntries = new List<string>();
        private static readonly object LockObj = new();

        public static void LogDecision(string module, string decision, string context = "")
        {
            var timestamp = DateTime.UtcNow.ToString("HH:mm:ss.fff");
            var entry = $"[{timestamp}] [{module}] Decision: {decision} {context}".Trim();

            lock (LockObj)
            {
                LogEntries.Add(entry);
            }

            DebugConsole?.WriteLine(entry);
        }

        public static void LogWarning(string module, string warning)
        {
            var timestamp = DateTime.UtcNow.ToString("HH:mm:ss.fff");
            var entry = $"[{timestamp}] [{module}] ⚠️ Warning: {warning}";

            lock (LockObj)
            {
                LogEntries.Add(entry);
            }

            DebugConsole?.WriteLine(entry);
        }

        public static void LogError(string module, string error)
        {
            var timestamp = DateTime.UtcNow.ToString("HH:mm:ss.fff");
            var entry = $"[{timestamp}] [{module}] ❌ Error: {error}";

            lock (LockObj)
            {
                LogEntries.Add(entry);
            }

            DebugConsole?.WriteLine(entry);
        }

        public static IReadOnlyList<string> GetLogs()
        {
            lock (LockObj)
            {
                return LogEntries.AsReadOnly();
            }
        }

        public static void Clear()
        {
            lock (LockObj)
            {
                LogEntries.Clear();
            }
        }

        public static IDebugConsole DebugConsole { get; set; } // Optional console target
    }
}
