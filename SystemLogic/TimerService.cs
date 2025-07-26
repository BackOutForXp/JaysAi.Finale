// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class TimerService : IDisposable
    {
        private readonly ConcurrentDictionary<string, Timer> _timers = new();
        private readonly ConcurrentDictionary<string, CancellationTokenSource> _cancellationTokens = new();

        public void Schedule(string key, TimeSpan interval, Action callback, bool autoReset = true)
        {
            Cancel(key);

            var cts = new CancellationTokenSource();
            _cancellationTokens[key] = cts;

            void ExecuteCallback(object? _)
            {
                if (cts.IsCancellationRequested) return;

                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TimerService] Error in callback for '{key}': {ex.Message}");
                }

                if (!autoReset)
                    Cancel(key);
            }

            var timer = new Timer(ExecuteCallback, null, interval, autoReset ? interval : Timeout.InfiniteTimeSpan);
            _timers[key] = timer;
        }

        public void Cancel(string key)
        {
            if (_timers.TryRemove(key, out var timer))
                timer.Dispose();

            if (_cancellationTokens.TryRemove(key, out var cts))
                cts.Cancel();
        }

        public void CancelAll()
        {
            foreach (var key in _timers.Keys)
                Cancel(key);
        }

        public bool IsRunning(string key) => _timers.ContainsKey(key);

        public void Dispose()
        {
            CancelAll();
        }
    }
}
