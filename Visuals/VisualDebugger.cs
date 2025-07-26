// Neural v3.0 — VisualDebugger.cs
using System;
using System.Collections.Concurrent;
using System.Text;

namespace JaysAi.Finale.Visuals
{
    public static class VisualDebugger
    {
        private static readonly ConcurrentQueue<string> _logQueue = new();
        private static readonly StringBuilder _buffer = new();

        public static bool Enabled { get; set; } = true;
        public static int MaxLines { get; set; } = 100;

        /// <summary>
        /// Appends a timestamped debug log message to the visual queue.
        /// </summary>
        public static void Log(string message)
        {
            if (!Enabled || string.IsNullOrWhiteSpace(message)) return;

            string timestamp = $"[{DateTime.Now:HH:mm:ss}] {message}";
            _logQueue.Enqueue(timestamp);

            // Trim overflow
            while (_logQueue.Count > MaxLines)
                _logQueue.TryDequeue(out _);
        }

        /// <summary>
        /// Returns the current visible debug text as a single string.
        /// </summary>
        public static string GetLogText()
        {
            _buffer.Clear();

            foreach (var line in _logQueue)
                _buffer.AppendLine(line);

            return _buffer.ToString();
        }

        /// <summary>
        /// Clears the log buffer.
        /// </summary>
        public static void Clear()
        {
            while (_logQueue.TryDequeue(out _)) { }
        }
    }
}
