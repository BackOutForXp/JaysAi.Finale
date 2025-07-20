//monarch v2.1
using System;
using System.Collections.Generic;
using System.Linq;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Aimbot
{
    public class TargetSelector
    {
        private readonly float screenWidth;
        private readonly float screenHeight;
        private readonly float maxFov;
        private readonly Func<FrameSnapshot, float> scoringFunction;

        public TargetSelector(float screenWidth, float screenHeight, float maxFov, Func<FrameSnapshot, float> scoringFunction)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.maxFov = maxFov;
            this.scoringFunction = scoringFunction;
        }

        public FrameSnapshot? SelectTarget(List<FrameSnapshot> enemies)
        {
            if (enemies == null || enemies.Count == 0)
                return null;

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;

            FrameSnapshot? bestTarget = null;
            float bestScore = float.MaxValue;

            foreach (var enemy in enemies)
            {
                float dx = enemy.X - centerX;
                float dy = enemy.Y - centerY;
                float distanceToCenter = MathF.Sqrt(dx * dx + dy * dy);

                if (distanceToCenter <= maxFov)
                {
                    float score = scoringFunction(enemy);
                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestTarget = enemy;
                    }
                }
            }

            return bestTarget;
        }
    }
}
