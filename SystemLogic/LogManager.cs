//monarch v2.0
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JaysAi.Finale.SystemLogic
{
    public static class LogManager
    {
        private static readonly ConcurrentQueue<string> _logQueue = new();
        private static readonly List<string> _logBuffer = new();
        private const int MaxBufferSize = 100;

        public static event Action<string>? OnLogUpdated;

        public static void Log(string message)
        {
            string timestamped = $"[{DateTime.Now:HH:mm:ss}] {message}";
            _logQueue.Enqueue(timestamped);
            TrimBuffer(timestamped);
            OnLogUpdated?.Invoke(timestamped);
        }

        private static void TrimBuffer(string newEntry)
        {
            _logBuffer.Add(newEntry);
            if (_logBuffer.Count > MaxBufferSize)
                _logBuffer.RemoveAt(0);
        }

        public static IReadOnlyList<string> GetLogs()
        {
            return _logBuffer.AsReadOnly();
        }
    }
}
