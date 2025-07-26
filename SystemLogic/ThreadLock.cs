// neural v3.0
using System;
using System.Threading;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class ThreadLock : IDisposable
    {
        private readonly object _syncRoot = new();
        private bool _isDisposed;

        public void Execute(Action action)
        {
            if (_isDisposed) return;

            lock (_syncRoot)
            {
                if (!_isDisposed)
                {
                    action();
                }
            }
        }

        public T Execute<T>(Func<T> func)
        {
            if (_isDisposed) return default!;

            lock (_syncRoot)
            {
                return _isDisposed ? default! : func();
            }
        }

        public void Dispose()
        {
            lock (_syncRoot)
            {
                _isDisposed = true;
            }
        }
    }
}
