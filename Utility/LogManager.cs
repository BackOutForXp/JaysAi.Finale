// Neural v3.1
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace JaysAi.Finale.Utility
{
    public static class LogManager
    {
        private static readonly string _logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string _logFile = Path.Combine(_logDir, $"JaysAi_{DateTime.Now:yyyyMMdd_HHmmss}.log");

        private static readonly ConcurrentQueue<string> _logQueue = new();
        private static readonly object _flushLock = new();
        private static bool _flushing = false;

        static LogManager()
        {
            Directory.CreateDirectory(_logDir);
        }

        public static void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string formatted = $"[{timestamp}] {message}";
            _logQueue.Enqueue(formatted);
            Debug.WriteLine(formatted);

            if (!_flushing)
                FlushAsync();
        }

        public static void LogWarning(string message) => Log("[WARN] " + message);
        public static void LogError(string message) => Log("[ERROR] " + message);
        public static void LogDebug(string message)
        {
#if DEBUG
            Log("[DEBUG] " + message);
#endif
        }

        private static void FlushAsync()
        {
            lock (_flushLock)
            {
                if (_flushing) return;
                _flushing = true;

                _ = Task.Run(() =>
                {
                    try
                    {
                        using var writer = new StreamWriter(_logFile, append: true, Encoding.UTF8);
                        while (_logQueue.TryDequeue(out var line))
                        {
                            writer.WriteLine(line);
                        }
                    }
                    finally
                    {
                        _flushing = false;
                    }
                });
            }
        }
    }
}
