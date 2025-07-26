// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class ThreadManager : IDisposable
    {
        private readonly ConcurrentDictionary<string, CancellationTokenSource> _threads = new();
        private readonly object _syncRoot = new();
        private bool _isDisposed;

        public void Start(string id, Action<CancellationToken> action)
        {
            if (_isDisposed) return;

            lock (_syncRoot)
            {
                Stop(id); // Stop existing thread with same ID if exists

                var cts = new CancellationTokenSource();
                _threads[id] = cts;

                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        action(cts.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        // Expected
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ThreadManager:{id}] Exception: {ex.Message}");
                    }
                }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
        }

        public void Stop(string id)
        {
            if (_threads.TryRemove(id, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
            }
        }

        public void StopAll()
        {
            lock (_syncRoot)
            {
                foreach (var kvp in _threads)
                {
                    kvp.Value.Cancel();
                    kvp.Value.Dispose();
                }
                _threads.Clear();
            }
        }

        public bool IsRunning(string id)
        {
            return _threads.ContainsKey(id);
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            lock (_syncRoot)
            {
                StopAll();
                _isDisposed = true;
            }
        }
    }
}
