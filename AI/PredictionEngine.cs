// neural v3.0
using System;
using System.Collections.Generic;
using System.Numerics;
using JaysAi.Finale.AI;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    public class PredictionEngine
    {
        private readonly Dictionary<int, List<FrameSnapshot>> _history = new();
        private readonly PredictionCache _cache = new();
        private readonly float _maxHistoryDuration = 0.5f; // seconds

        public void UpdateHistory(TrackedTarget target, float currentTime)
        {
            if (!_history.TryGetValue(target.Id, out var list))
                _history[target.Id] = list = new List<FrameSnapshot>();

            list.Add(new FrameSnapshot
            {
                Position = target.Position,
                Velocity = target.Velocity,
                Time = currentTime
            });

            list.RemoveAll(f => currentTime - f.Time > _maxHistoryDuration);
        }

        public Vector3 PredictPosition(TrackedTarget target, float currentTime, float extrapolationTime = 0.15f)
        {
            UpdateHistory(target, currentTime);

            // Try to use cached result if valid
            var cached = _cache.Get(target.Id, currentTime);
            if (cached.HasValue)
                return cached.Value;

            var velocity = target.Velocity;

            // Simple linear prediction
            var predicted = target.Position + velocity * extrapolationTime;

            _cache.Store(target.Id, predicted, currentTime);
            return predicted;
        }

        public void Clear()
        {
            _history.Clear();
            _cache.ClearAll();
        }

        public void ClearExpired(float currentTime)
        {
            _cache.ClearOld(currentTime);

            foreach (var key in new List<int>(_history.Keys))
            {
                _history[key].RemoveAll(s => currentTime - s.Time > _maxHistoryDuration);
                if (_history[key].Count == 0)
                    _history.Remove(key);
            }
        }
    }
}
