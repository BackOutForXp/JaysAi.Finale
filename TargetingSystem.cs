using JaysAi.Finale.AI;
using JaysAi.Finale.Math;
using JaysAi.Finale.Targeting;
using JaysAi.Finale.Utility;
using System.Collections.Generic;

namespace JaysAi.Finale
{
    public static class TargetingSystem
    {
        public static Enemy? GetBestTarget(List<Enemy> enemies, TargetProfileManager profileManager)
        {
            Enemy? best = null;
            float bestScore = 0f;

            foreach (var enemy in enemies)
            {
                if (!enemy.IsVisible || enemy.ScreenPosition == null)
                    continue;

                var profile = profileManager.GetOrCreate(enemy.Id);
                float score = SnapScore.Calculate(enemy.ScreenPosition.Value, profile);

                if (score > bestScore)
                {
                    bestScore = score;
                    best = enemy;
                }
            }

            return best;
        }
    }
}
