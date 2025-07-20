//monarch v2.1
using System;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Aimbot
{
    public class SnapLogic
    {
        private readonly SnapConfig config;

        public SnapLogic(SnapConfig config)
        {
            this.config = config;
        }

        public AimAdjustment CalculateSnap(FrameSnapshot target, float currentX, float currentY)
        {
            float dx = target.X - currentX;
            float dy = target.Y - currentY;

            float distance = MathF.Sqrt(dx * dx + dy * dy);
            if (distance > config.MagnetThreshold)
                return AimAdjustment.None;

            float adjustedX = dx * config.SnapStrength;
            float adjustedY = dy * config.VerticalLock ? 0 : dy * config.SnapStrength;

            return new AimAdjustment(adjustedX, adjustedY);
        }
    }

    public readonly struct AimAdjustment
    {
        public readonly float DeltaX;
        public readonly float DeltaY;

        public static readonly AimAdjustment None = new AimAdjustment(0, 0);

        public AimAdjustment(float dx, float dy)
        {
            DeltaX = dx;
            DeltaY = dy;
        }

        public bool IsZero => DeltaX == 0 && DeltaY == 0;
    }
}
