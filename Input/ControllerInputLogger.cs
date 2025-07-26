//neural v3.0
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JaysAi.Finale.Input.Models;

namespace JaysAi.Finale.Input
{
    public class ControllerInputLogger : IDisposable
    {
        private readonly string _logFilePath;
        private readonly ConcurrentQueue<string> _logQueue;
        private readonly CancellationTokenSource _cts;
        private readonly Task _loggingTask;
        private bool _disposed;

        public ControllerInputLogger(string logDirectory = "Logs")
        {
            Directory.CreateDirectory(logDirectory);
            _logFilePath = Path.Combine(logDirectory, $"controller_log_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt");

            _logQueue = new ConcurrentQueue<string>();
            _cts = new CancellationTokenSource();
            _loggingTask = Task.Run(ProcessQueueAsync);
        }

        public void LogInput(int controllerId, ControllerInputState state)
        {
            var logEntry = $"{DateTime.UtcNow:O} | Controller {controllerId} | {state}";
            _logQueue.Enqueue(logEntry);
        }

        private async Task ProcessQueueAsync()
        {
            using var writer = new StreamWriter(_logFilePath, append: true, Encoding.UTF8);
            while (!_cts.IsCancellationRequested || !_logQueue.IsEmpty)
            {
                while (_logQueue.TryDequeue(out var entry))
                {
                    await writer.WriteLineAsync(entry);
                }

                await Task.Delay(50); // Throttle write frequency
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _cts.Cancel();
            _loggingTask.Wait();
            _cts.Dispose();

            _disposed = true;
        }
    }
}
