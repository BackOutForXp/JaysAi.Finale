// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JaysAi.Finale.AI;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.SystemLogic
{
    public static class TargetLogger
    {
        private static readonly ConcurrentDictionary<Guid, TargetLogEntry> _targetLog = new();
        private static readonly object _lock = new();

        public static void LogTarget(TargetData targetData)
        {
            if (targetData == null || string.IsNullOrWhiteSpace(targetData.Label)) return;

            var entry = new TargetLogEntry
            {
                Id = Guid.NewGuid(),
                Label = targetData.Label,
                Confidence = targetData.Confidence,
                Timestamp = DateTime.UtcNow,
                Position = targetData.Position
            };

            _targetLog[entry.Id] = entry;

            Log.Info($"[TargetLogger] Target logged: {entry.Label} ({entry.Confidence:P1}) at {entry.Position}");
        }

        public static IReadOnlyCollection<TargetLogEntry> GetAllEntries()
        {
            return _targetLog.Values;
        }

        public static void Clear()
        {
            _targetLog.Clear();
        }
    }

    public class TargetLogEntry
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public float Confidence { get; set; }
        public DateTime Timestamp { get; set; }
        public System.Numerics.Vector2 Position { get; set; }
    }
}
