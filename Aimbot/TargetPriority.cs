//monarch v2.1
using System;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Aimbot
{
    public static class TargetPriority
    {
        // Returns the enemy closest to the center of screen
        public static Func<FrameSnapshot, float> ByCrosshair(float screenCenterX, float screenCenterY)
        {
            return enemy =>
            {
                float dx = enemy.X - screenCenterX;
                float dy = enemy.Y - screenCenterY;
                return dx * dx + dy * dy;
            };
        }

        // Score based on distance (Z-depth)
        public static Func<FrameSnapshot, float> ByDistance()
        {
            return enemy => enemy.Distance;
        }

        // Score based on low health (useful for cleanup targeting)
        public static Func<FrameSnapshot, float> ByLowHealth()
        {
            return enemy => enemy.Health > 0 ? 100 - enemy.Health : float.MaxValue;
        }

        // Score based on movement speed (avoid fast movers or favor them)
        public static Func<FrameSnapshot, float> BySpeed()
        {
            return enemy => enemy.Velocity.Length;
        }

        // Combine multiple strategies: FOV + Health + Distance
        public static Func<FrameSnapshot, float> Composite(float screenCenterX, float screenCenterY)
        {
            return enemy =>
            {
                float dx = enemy.X - screenCenterX;
                float dy = enemy.Y - screenCenterY;
                float fovScore = dx * dx + dy * dy;
                float healthScore = 100 - enemy.Health;
                float distanceScore = enemy.Distance;

                return fovScore + healthScore + distanceScore * 0.5f;
            };
        }
    }
}
