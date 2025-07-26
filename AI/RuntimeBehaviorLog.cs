// Neural v3.1 — RuntimeBehaviorLog.cs
using JaysAi.Finale.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;

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

        public void LogUpdate(List<TrackedTarget> targets, Dictionary<int, Vector3> predictions)
        {
            foreach (var target in targets)
            {
                if (predictions.TryGetValue(target.ID, out var predicted))
                {
                    Log("Prediction", $"Predicted Position={predicted}", target.Enemy);
                }

                Log("Tracking", $"Visible={target.IsVisible} Smoothed={target.SmoothedPosition}", target.Enemy);
            }
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

        public void StartSession() => Log("Session", "AI Runtime session started");
        public void EndSession() => Log("Session", "AI Runtime session ended");
    }
}
