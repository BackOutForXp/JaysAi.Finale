//heavenly v3.0
using JaysAi.Finale.Modules;
using System;

namespace JaysAi.Finale.AI
{
    public static class SnapScore
    {
        public static float Calculate(TargetInfo target, AimContext context)
        {
            if (target == null || context == null) return 0f;

            float distanceScore = 1f - Math.Clamp(target.Distance / context.MaxRange, 0f, 1f);
            float visibilityScore = target.IsVisible ? 1f : 0.2f;
            float movementScore = 1f - Math.Clamp(target.Velocity.Magnitude / context.MaxTargetSpeed, 0f, 1f);

            float centerOffset = Math.Abs(target.ScreenX - context.CrosshairX) + Math.Abs(target.ScreenY - context.CrosshairY);
            float centeringScore = 1f - Math.Clamp(centerOffset / context.MaxOffsetTolerance, 0f, 1f);

            // Weighted sum
            float score = (distanceScore * 0.4f) +
                          (visibilityScore * 0.2f) +
                          (movementScore * 0.2f) +
                          (centeringScore * 0.2f);

            return score;
        }
    }

    public class AimContext
    {
        public float MaxRange { get; set; } = 100f;
        public float MaxTargetSpeed { get; set; } = 20f;
        public float MaxOffsetTolerance { get; set; } = 200f;

        public float CrosshairX { get; set; }
        public float CrosshairY { get; set; }
    }
}
