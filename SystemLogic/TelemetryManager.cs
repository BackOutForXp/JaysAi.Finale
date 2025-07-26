// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Timers;
using JaysAi.Finale.SystemLogic.Models;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class TelemetryManager : IDisposable
    {
        private readonly Timer _flushTimer;
        private readonly ConcurrentQueue<TelemetryEntry> _entryQueue = new();
        private readonly List<TelemetryEntry> _batchedEntries = new();
        private readonly object _syncLock = new();
        private bool _disposed;

        public event Action<IReadOnlyList<TelemetryEntry>>? OnFlush;

        public TelemetryManager(double flushIntervalMs = 1000)
        {
            _flushTimer = new Timer(flushIntervalMs);
            _flushTimer.Elapsed += (_, _) => Flush();
            _flushTimer.Start();
        }

        public void Log(string category, string message)
        {
            _entryQueue.Enqueue(new TelemetryEntry
            {
                Timestamp = DateTime.UtcNow,
                Category = category,
                Message = message
            });
        }

        private void Flush()
        {
            if (_disposed) return;

            lock (_syncLock)
            {
                while (_entryQueue.TryDequeue(out var entry))
                    _batchedEntries.Add(entry);

                if (_batchedEntries.Count > 0)
                {
                    OnFlush?.Invoke(_batchedEntries.AsReadOnly());
                    _batchedEntries.Clear();
                }
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _flushTimer.Stop();
            _flushTimer.Dispose();
            Flush();
        }
    }
}
