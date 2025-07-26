// Neural v3.1 — MotionTracker.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.AI
{
    public class MotionTracker
    {
        private readonly Dictionary<int, List<MotionSample>> _targetHistory = new();
        private const int MaxHistory = 20;

        public void Track(int targetId, Vector2 position)
        {
            if (!_targetHistory.ContainsKey(targetId))
                _targetHistory[targetId] = new List<MotionSample>();

            var history = _targetHistory[targetId];
            var now = TimeUtils.Now();

            history.Add(new MotionSample { Time = now, Position = position });

            if (history.Count > MaxHistory)
                history.RemoveAt(0);
        }

        public MotionStats GetMotionStats(int targetId)
        {
            if (!_targetHistory.TryGetValue(targetId, out var history) || history.Count < 2)
                return new MotionStats();

            var first = history[0];
            var last = history[^1];
            var deltaTime = (float)(last.Time - first.Time).TotalSeconds;
            if (deltaTime <= 0f) return new MotionStats();

            var deltaPos = last.Position - first.Position;
            var velocity = deltaPos / deltaTime;

            Vector2 acceleration = Vector2.Zero;
            if (history.Count >= 3)
            {
                var mid = history[history.Count / 2];
                var deltaMidTime = (float)(last.Time - mid.Time).TotalSeconds;
                if (deltaMidTime > 0f)
                {
                    var deltaMidPos = last.Position - mid.Position;
                    var midVelocity = deltaMidPos / deltaMidTime;
                    acceleration = (midVelocity - velocity) / deltaMidTime;
                }
            }

            return new MotionStats
            {
                Velocity = velocity,
                Acceleration = acceleration,
                SampleCount = history.Count
            };
        }

        public void Clear()
        {
            _targetHistory.Clear();
        }

        public void Initialize()
        {
            Clear();
        }

        public void ProcessMovementData()
        {
            // Optional: Add logic to prune stale targets or average group motion
        }
    }
}
