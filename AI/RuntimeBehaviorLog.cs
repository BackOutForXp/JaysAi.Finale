// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    public class RuntimeBehaviorLog
    {
        private readonly ConcurrentQueue<BehaviorEntry> _logQueue;
        private readonly int _maxEntries;

        public RuntimeBehaviorLog(int maxEntries = 250)
        {
            _maxEntries = maxEntries;
            _logQueue = new ConcurrentQueue<BehaviorEntry>();
        }

        public void Log(string behaviorType, string details, Enemy? target = null)
        {
            if (_logQueue.Count >= _maxEntries)
                _logQueue.TryDequeue(out _);

            _logQueue.Enqueue(new BehaviorEntry
            {
                Timestamp = DateTime.UtcNow,
                BehaviorType = behaviorType,
                Details = details,
                TargetId = target?.ID ?? -1
            });
        }

        public IEnumerable<BehaviorEntry> GetRecentEntries(int limit = 50)
        {
            var list = new List<BehaviorEntry>(_logQueue);
            return list.Count > limit ? list.GetRange(list.Count - limit, limit) : list;
        }

        public void Clear()
        {
            while (_logQueue.TryDequeue(out _)) { }
        }

        public record BehaviorEntry
        {
            public DateTime Timestamp { get; init; }
            public string BehaviorType { get; init; } = string.Empty;
            public string Details { get; init; } = string.Empty;
            public int TargetId { get; init; }
        }
    }
}
