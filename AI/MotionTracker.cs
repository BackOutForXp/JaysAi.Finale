//heavenly v3.0 – Motion Estimation & Smoothing
using System;
using System.Collections.Generic;
using JaysAi.Finale.AI;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.AI
{
    public class MotionTracker
    {
        private readonly Dictionary<int, Queue<(float X, float Y, long Timestamp)>> _motionHistory = new();
        private const int MaxSamples = 10;
        private const float SmoothingFactor = 0.6f;

        public void RecordPosition(int id, float x, float y)
        {
            var timestamp = DateTime.UtcNow.Ticks;

            if (!_motionHistory.ContainsKey(id))
                _motionHistory[id] = new Queue<(float, float, long)>();

            var history = _motionHistory[id];
            history.Enqueue((x, y, timestamp));

            while (history.Count > MaxSamples)
                history.Dequeue();
        }

        public (float X, float Y)? GetSmoothedPosition(int id)
        {
            if (!_motionHistory.TryGetValue(id, out var history) || history.Count == 0)
                return null;

            float smoothedX = 0;
            float smoothedY = 0;
            float weight = 1;
            float totalWeight = 0;

            foreach (var (x, y, _) in history)
            {
                smoothedX += x * weight;
                smoothedY += y * weight;
                totalWeight += weight;
                weight *= SmoothingFactor;
            }

            return (smoothedX / totalWeight, smoothedY / totalWeight);
        }

        public void Reset(int id)
        {
            if (_motionHistory.ContainsKey(id))
                _motionHistory[id].Clear();
        }

        public void Clear()
        {
            _motionHistory.Clear();
        }
    }
}
