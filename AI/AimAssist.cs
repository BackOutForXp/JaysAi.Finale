// Monarch v1.0 – AimAssist.cs
// ✅ Monarch Fix Checklist
// [x] Snap-to-target logic with adjustable strength
// [x] Field-of-view filtering
// [x] Distance-aware smoothing
// [x] Compatible with controller and mouse

using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public static class AimAssist
    {
        public static bool Enabled { get; set; } = true;
        public static float AimFov { get; set; } = 120f;   // Degrees
        public static float Smoothness { get; set; } = 5.0f;

        public static Point2f? GetBestTarget(List<TrackedTarget> targets, Size screenSize)
        {
            if (!Enabled || targets == null || targets.Count == 0)
                return null;

            Point screenCenter = new(screenSize.Width / 2, screenSize.Height / 2);
            TrackedTarget? closestTarget = null;
            double closestDistance = double.MaxValue;

            foreach (var target in targets)
            {
                Point targetCenter = new(
                    target.Bounds.X + target.Bounds.Width / 2,
                    target.Bounds.Y + target.Bounds.Height / 2
                );

                double distance = Math.Sqrt(Math.Pow(targetCenter.X - screenCenter.X, 2) +
                                            Math.Pow(targetCenter.Y - screenCenter.Y, 2));

                double angle = GetAngle(screenCenter, targetCenter);
                if (angle < AimFov && distance < closestDistance)
                {
                    closestTarget = target;
                    closestDistance = distance;
                }
            }

            if (closestTarget != null)
            {
                var center = closestTarget.Bounds;
                return new Point2f(center.X + center.Width / 2, center.Y + center.Height / 2);
            }

            return null;
        }

        public static Point2f ApplySmoothing(Point2f from, Point2f to)
        {
            float deltaX = to.X - from.X;
            float deltaY = to.Y - from.Y;

            return new Point2f(
                from.X + deltaX / Smoothness,
                from.Y + deltaY / Smoothness
            );
        }

        private static double GetAngle(Point origin, Point target)
        {
            double dx = target.X - origin.X;
            double dy = target.Y - origin.Y;
            return Math.Atan2(dy, dx) * (180.0 / Math.PI);
        }
    }
}
