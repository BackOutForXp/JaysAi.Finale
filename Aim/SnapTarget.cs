//monarch v2.0
using JaysAi.AI;
using JaysAi.Finale.Modules;
using JaysAi.SystemLogic;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public static class SnapTarget
    {
        public static EntityData? GetBestTarget(List<EntityData> enemies)
        {
            EntityData? best = null;
            float bestScore = float.MaxValue;
            Vector2 screenCenter = ScreenUtils.GetCenter();

            foreach (var enemy in enemies)
            {
                if (enemy.ScreenPosition == Vector2.Zero)
                    continue;

                float distance = Vector2.Distance(screenCenter, enemy.ScreenPosition);
                if (distance > SnapSettings.MaxSnapRange)
                    continue;

                if (distance < bestScore)
                {
                    bestScore = distance;
                    best = enemy;
                }
            }

            return best;
        }
    }
}
