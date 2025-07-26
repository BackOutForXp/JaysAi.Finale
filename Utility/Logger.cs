// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale.Utility
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }

    public static class Logger
    {
        private static readonly BlockingCollection<string> _logQueue = new();
        private static readonly string _logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
        private static readonly string _logFilePath;
        private static readonly CancellationTokenSource _cts = new();
        private static readonly Task _logTask;
        private static LogLevel _minimumLevel = LogLevel.Info;

        static Logger()
        {
            Directory.CreateDirectory(_logDirectory);
            _logFilePath = Path.Combine(_logDirectory, $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            _logTask = Task.Factory.StartNew(() =>
            {
                foreach (var message in _logQueue.GetConsumingEnumerable(_cts.Token))
                {
                    try
                    {
                        File.AppendAllText(_logFilePath, message + Environment.NewLine, Encoding.UTF8);
                    }
                    catch (IOException) { /* Suppress logging IO exceptions */ }
                }
            }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public static void SetLogLevel(LogLevel level)
        {
            _minimumLevel = level;
        }

        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            if (level < _minimumLevel) return;

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var entry = $"[{timestamp}] [{level}] {message}";

            _logQueue.Add(entry);

#if DEBUG
            Debug.WriteLine(entry);
#endif
        }

        public static void LogException(Exception ex, string context = "")
        {
            Log($"{context} Exception: {ex.Message}\n{ex.StackTrace}", LogLevel.Error);
        }

        public static void Shutdown()
        {
            _cts.Cancel();
            _logQueue.CompleteAdding();
            try
            {
                _logTask.Wait(2000);
            }
            catch (AggregateException) { }
        }
    }
}
