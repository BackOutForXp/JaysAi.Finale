//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public static class TargetEvaluator
    {
        public static SnapTarget EvaluateBestTarget(List<TrackedTarget> targets, Vector2 crosshairPosition, float maxSnapDistance)
        {
            SnapTarget best = null;
            float bestScore = float.MinValue;

            foreach (var target in targets)
            {
                if (!target.IsAlive || !target.IsVisible)
                    continue;

                Vector2 screenPos = target.ScreenPosition;
                float distance = Vector2.Distance(screenPos, crosshairPosition);
                if (distance > maxSnapDistance)
                    continue;

                float score = CalculateTargetScore(target, distance);
                if (score > bestScore)
                {
                    bestScore = score;
                    best = new SnapTarget(target, screenPos, distance, score, target.IsVisible);
                }
            }

            return best;
        }

        private static float CalculateTargetScore(TrackedTarget target, float distance)
        {
            float healthFactor = target.Health > 0 ? 1f : 0f;
            float distanceWeight = 1f / (distance + 1f); // Avoid division by zero
            float priority = target.PriorityScore;

            return (distanceWeight * 0.6f) + (priority * 0.3f) + (healthFactor * 0.1f);
        }
    }
}
