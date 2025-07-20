//monarch v2.0
using JaysAi.AI;
using JaysAi.Finale.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public static class TargetEvaluator
    {
        public static EntityData GetBestTarget(List<EntityData> visibleEntities)
        {
            if (visibleEntities == null || visibleEntities.Count == 0)
                return null;

            EntityData bestTarget = null;
            float bestScore = float.MinValue;

            foreach (var entity in visibleEntities)
            {
                if (SnapSettings.IgnoreTeamTargets && TeamColorDetector.IsFriendly(entity))
                    continue;

                if (SnapSettings.RequireLineOfSight && !LineOfSightChecker.HasClearView(entity))
                    continue;

                float score = CalculateScore(entity);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestTarget = entity;
                }
            }

            return bestTarget;
        }

        private static float CalculateScore(EntityData entity)
        {
            Vector2 screenCenter = ScreenUtils.GetCenter();
            float distance = Vector2.Distance(entity.ScreenPosition, screenCenter);
            float fov = MathF.Abs(distance);

            float fovWeight = 1f;
            float proximityWeight = 1.2f;

            return 1f / (fov + 1) * fovWeight + 1f / (distance + 1) * proximityWeight;
        }
    }
}
