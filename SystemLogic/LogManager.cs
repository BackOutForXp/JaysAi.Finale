// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.SystemLogic
{
    public static class LogManager
    {
        private static readonly ConcurrentQueue<string> LogQueue = new();
        private static readonly AutoResetEvent LogSignal = new(false);
        private static readonly string LogDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
        private static readonly string LogFile = Path.Combine(LogDirectory, $"log_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt");

        private static Thread? _logWorker;
        private static bool _isRunning;

        public static void Initialize()
        {
            if (_isRunning) return;
            _isRunning = true;

            if (!Directory.Exists(LogDirectory))
                Directory.CreateDirectory(LogDirectory);

            _logWorker = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "LogManagerWorker"
            };
            _logWorker.Start();
        }

        public static void Shutdown()
        {
            _isRunning = false;
            LogSignal.Set();
            _logWorker?.Join();
        }

        public static void Write(string message, string? tag = null)
        {
            if (!_isRunning) Initialize();

            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var formatted = string.IsNullOrWhiteSpace(tag)
                ? $"[{timestamp}] {message}"
                : $"[{timestamp}][{tag}] {message}";

            LogQueue.Enqueue(formatted);
            LogSignal.Set();
        }

        private static void ProcessLogQueue()
        {
            using var writer = new StreamWriter(LogFile, append: true);
            while (_isRunning || !LogQueue.IsEmpty)
            {
                while (LogQueue.TryDequeue(out var log))
                {
                    writer.WriteLine(log);
                    writer.Flush();
                }
                LogSignal.WaitOne(100);
            }
        }

        public static void LogException(Exception ex, string? tag = "EXCEPTION")
        {
            Write($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}", tag);
        }

        public static void LogStartupStatus()
        {
            Write("JaysAi Loader Started", "BOOT");
            Write($"System Time: {DateTime.Now}", "BOOT");
            Write($"Machine: {Environment.MachineName}, User: {Environment.UserName}", "BOOT");
        }
    }
}
