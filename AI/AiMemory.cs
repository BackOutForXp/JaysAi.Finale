//heavenly v3.0 – AI Memory Log for Target Tracking
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class AiMemoryEntry
    {
        public int TargetId { get; set; }
        public Vector2 Position { get; set; }
        public float Timestamp { get; set; }
        public Vector2 Velocity { get; set; }
    }

    public static class AiMemory
    {
        private static readonly Dictionary<int, List<AiMemoryEntry>> memoryLog = new();
        private const int MaxEntriesPerTarget = 60;

        public static void Log(int targetId, Vector2 position, float timestamp)
        {
            if (!memoryLog.ContainsKey(targetId))
                memoryLog[targetId] = new List<AiMemoryEntry>();

            var entries = memoryLog[targetId];

            if (entries.Count > 0)
            {
                var last = entries[^1];
                var velocity = (position - last.Position) / (timestamp - last.Timestamp);
                entries.Add(new AiMemoryEntry
                {
                    TargetId = targetId,
                    Position = position,
                    Timestamp = timestamp,
                    Velocity = velocity
                });
            }
            else
            {
                entries.Add(new AiMemoryEntry
                {
                    TargetId = targetId,
                    Position = position,
                    Timestamp = timestamp,
                    Velocity = Vector2.Zero
                });
            }

            if (entries.Count > MaxEntriesPerTarget)
                entries.RemoveAt(0);
        }

        public static Vector2? GetSmoothedVelocity(int targetId)
        {
            if (!memoryLog.ContainsKey(targetId) || memoryLog[targetId].Count < 2)
                return null;

            var entries = memoryLog[targetId];
            Vector2 total = Vector2.Zero;
            int count = 0;

            for (int i = Math.Max(0, entries.Count - 5); i < entries.Count; i++)
            {
                total += entries[i].Velocity;
                count++;
            }

            return count > 0 ? total / count : null;
        }

        public static void Clear() => memoryLog.Clear();
    }
}
