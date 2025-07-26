// Neural v3.1 — TargetingSystem.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JaysAi.Finale.Data;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public class TargetingSystem
    {
        private readonly List<TargetInfo> _targets = new();
        private readonly float _maxDistance = 150f;
        private readonly float _visibilityBonus = 1.5f;
        private readonly float _priorityDecay = 0.98f;

        public void Initialize()
        {
            _targets.Clear();
        }

        public void EvaluateTargets(List<TrackedTarget> trackedTargets)
        {
            _targets.Clear();

            foreach (var tracked in trackedTargets)
            {
                var enemy = tracked.Enemy;
                if (enemy == null || !enemy.IsVisible || !enemy.IsTracked)
                    continue;

                float distance = enemy.Distance;
                float movement = CalculateMovementScore(tracked);
                float alignment = CalculateAlignmentScore(tracked);

                var info = new TargetInfo(enemy, enemy.HeadPosition)
                {
                    Distance = distance,
                    IsVisible = enemy.IsVisible,
                    AimWeightScore = CalculateScore(distance, movement, alignment),
                    VisibilityScore = enemy.IsVisible ? 1.0f : 0.0f
                };

                _targets.Add(info);
            }
        }

        public TargetInfo GetPrimaryTarget()
        {
            return _targets.OrderByDescending(t => t.AimWeightScore).FirstOrDefault();
        }

        private float CalculateScore(float distance, float movement, float alignment)
        {
            float distScore = Math.Clamp(100f - distance, 0, 100);
            float moveScore = Math.Clamp(100f - movement, 0, 100);
            float alignScore = Math.Clamp(alignment, 0, 100);

            float score = (distScore + moveScore + alignScore) / 3f;
            score *= _priorityDecay;
            return score;
        }

        private float CalculateMovementScore(TrackedTarget tracked)
        {
            if (tracked.PositionHistory.Count < 2)
                return 0f;

            var history = tracked.PositionHistory.ToArray();
            return Vector3.Distance(history[^1], history[^2]);
        }

        private float CalculateAlignmentScore(TrackedTarget tracked)
        {
            // Placeholder for crosshair alignment logic
            // Will use dot product of aim vector vs. enemy vector in final overlay phase
            return 50f; // temp static
        }

        public List<TargetInfo> GetAllScoredTargets() => _targets;
    }
}
