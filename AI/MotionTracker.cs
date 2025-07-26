// neural v3.0
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
            if (!_targetHistory.ContainsKey(targetId)) return new MotionStats();

            var history = _targetHistory[targetId];
            if (history.Count < 2) return new MotionStats();

            var first = history[0];
            var last = history[^1];

            var deltaTime = (float)(last.Time - first.Time).TotalSeconds;
            if (deltaTime <= 0) return new MotionStats();

            var deltaPos = last.Position - first.Position;
            var velocity = deltaPos / deltaTime;

            var acceleration = Vector2.Zero;
            if (history.Count >= 3)
            {
                var mid = history[^2];
                var dtMid = (float)(last.Time - mid.Time).TotalSeconds;
                var vMid = (last.Position - mid.Position) / dtMid;

                acceleration = (velocity - vMid) / dtMid;
            }

            return new MotionStats
            {
                Velocity = velocity,
                Acceleration = acceleration,
                DirectionAngle = MathF.Atan2(velocity.Y, velocity.X) * (180f / MathF.PI),
                SampleCount = history.Count
            };
        }

        public void Reset(int targetId)
        {
            if (_targetHistory.ContainsKey(targetId))
                _targetHistory[targetId].Clear();
        }

        private class MotionSample
        {
            public DateTime Time { get; set; }
            public Vector2 Position { get; set; }
        }
    }

    public class MotionStats
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Acceleration { get; set; } = Vector2.Zero;
        public float DirectionAngle { get; set; } = 0f;
        public int SampleCount { get; set; } = 0;
    }
}
