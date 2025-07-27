// Neural v3.1
using JaysAi.Finale.Data;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Models;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Aimbot
{
    public class PredictionEngine
    {
        private readonly Dictionary<Guid, MotionSample> _previousSamples = new();
        public Dictionary<Guid, Vector3> LatestPredictions { get; private set; } = new();

        public void Initialize()
        {
            _previousSamples.Clear();
            LatestPredictions.Clear();
        }

        public void UpdatePredictions(IEnumerable<TrackedTarget> targets)
        {
            LatestPredictions.Clear();
            float latency = LatencyHelper.GetCurrentLatencyMs(); // Average ping or user-estimate
            float deltaTime = TimeUtils.DeltaTime;

            foreach (var target in targets)
            {
                if (!_previousSamples.TryGetValue(target.Id, out var previous))
                {
                    _previousSamples[target.Id] = new MotionSample
                    {
                        Position = target.WorldPosition,
                        Timestamp = DateTime.UtcNow
                    };
                    continue;
                }

                float elapsed = (float)(DateTime.UtcNow - previous.Timestamp).TotalSeconds;
                Vector3 velocity = PredictionHelper.EstimateVelocity(previous.Position, target.WorldPosition, elapsed);

                Vector3 predicted = PredictionHelper.PredictFuturePosition(target.WorldPosition, velocity, latency);
                LatestPredictions[target.Id] = predicted;

                // Update for next round
                _previousSamples[target.Id] = new MotionSample
                {
                    Position = target.WorldPosition,
                    Timestamp = DateTime.UtcNow
                };
            }
        }
    }
}
