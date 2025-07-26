// Neural v3.1 — PredictionEngine.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class PredictionEngine
    {
        private readonly Dictionary<int, List<FrameSnapshot>> _history = new();
        private readonly PredictionCache _cache = new();
        private readonly float _maxHistoryDuration = 0.5f; // seconds

        public Dictionary<int, Vector3> LatestPredictions { get; private set; } = new();

        public void Initialize()
        {
            _history.Clear();
            LatestPredictions.Clear();
        }

        public void UpdatePredictions(List<TrackedTarget> targets)
        {
            float currentTime = TimeUtils.GetTime();

            foreach (var target in targets)
            {
                UpdateHistory(target, currentTime);

                Vector3 predicted = PredictPosition(target, currentTime, 0.15f);
                LatestPredictions[target.Id] = predicted;

                target.SetPredictedPosition(predicted);
            }
        }

        private void UpdateHistory(TrackedTarget target, float currentTime)
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

        private Vector3 PredictPosition(TrackedTarget target, float currentTime, float extrapolationTime = 0.15f)
        {
            UpdateHistory(target, currentTime);

            var cached = _cache.Get(target.Id, currentTime);
            if (cached.HasValue)
                return cached.Value;

            var velocity = target.Velocity;
            var predicted = target.Position + velocity * extrapolationTime;

            _cache.Store(target.Id, predicted, currentTime);
            return predicted;
        }
    }
}
