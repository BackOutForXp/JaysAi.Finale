// Neural v3.0 — OverlayLogger.cs
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Visuals
{
    public static class OverlayLogger
    {
        private static readonly List<string> _log = new();

        public static void Log(string message)
        {
            var timestamped = $"[{DateTime.Now:HH:mm:ss}] {message}";
            _log.Add(timestamped);
            if (_log.Count > 1000) _log.RemoveAt(0);
        }

        public static IEnumerable<string> GetLogs() => _log.ToArray();

        public static void Clear() => _log.Clear();
    }
}
