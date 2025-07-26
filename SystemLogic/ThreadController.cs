// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class ThreadController : IDisposable
    {
        private readonly ConcurrentDictionary<string, CancellationTokenSource> _threads = new();
        private bool _disposed;

        public void StartOrReplace(string name, Action<CancellationToken> taskAction)
        {
            Stop(name);

            var cts = new CancellationTokenSource();
            _threads[name] = cts;

            Task.Factory.StartNew(() => taskAction(cts.Token),
                cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Stop(string name)
        {
            if (_threads.TryRemove(name, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
            }
        }

        public void StopAll()
        {
            foreach (var key in _threads.Keys)
            {
                Stop(key);
            }
        }

        public bool IsRunning(string name)
        {
            return _threads.ContainsKey(name);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            StopAll();
        }
    }
}
