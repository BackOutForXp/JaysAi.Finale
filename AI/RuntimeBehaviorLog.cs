//monarch v2.1 – Real-Time AI Behavior Logging
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public static class RuntimeBehaviorLog
    {
        private static readonly List<string> _logEntries = new();
        private const int MaxEntries = 100;

        public static void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string entry = $"[{timestamp}] {message}";

            _logEntries.Add(entry);
            if (_logEntries.Count > MaxEntries)
            {
                _logEntries.RemoveAt(0);
            }

            Console.WriteLine(entry); // Optional: pipe to overlay or file
        }

        public static IEnumerable<string> GetEntries()
        {
            return _logEntries;
        }

        public static void Clear()
        {
            _logEntries.Clear();
        }
    }
}
