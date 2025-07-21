//monarch v2.1 – Target Prioritization Engine
using JaysAi.Finale.AI;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.Aimbot
{
    public static class TargetSelector
    {
        public static DetectionObject? GetBestTarget(IEnumerable<DetectionObject> detectedObjects)
        {
            if (detectedObjects == null) return null;

            return detectedObjects
                .Where(obj => obj.IsEnemy)
                .OrderBy(obj => GetDistanceToCenter(obj))
                .FirstOrDefault();
        }

        private static double GetDistanceToCenter(DetectionObject obj)
        {
            var centerX = ScreenHelper.CenterX;
            var centerY = ScreenHelper.CenterY;

            var targetX = obj.X + obj.Width / 2;
            var targetY = obj.Y + obj.Height / 2;

            var dx = targetX - centerX;
            var dy = targetY - centerY;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
