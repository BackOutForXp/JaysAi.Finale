// Monarch v1.0 – AimbotLogic.cs
// ✅ Monarch Fix Checklist
// [x] Smart target prioritization (FOV-based)
// [x] Adjustable aim offset with smoothing + deadzone
// [x] Works with controller stick or mouse logic

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Modules;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public static class AimbotLogic
    {
        public static DetectedObject? FindBestTarget(IReadOnlyList<DetectedObject> enemies)
        {
            Vector2 screenCenter = new(960, 540); // Adjust based on resolution

            return enemies
                .Select(e => new { Enemy = e, Distance = Vector2.Distance(e.Center2D.ToVector2(), screenCenter) })
                .Where(e => e.Distance < SnapSettings.SnapFOV * 1000f)
                .OrderBy(e => e.Distance)
                .Select(e => e.Enemy)
                .FirstOrDefault();
        }

        public static Vector2 CalculateAimOffset(DetectedObject target)
        {
            Vector2 screenCenter = new(960, 540);
            Vector2 targetPos = target.Center2D.ToVector2();
            Vector2 rawOffset = targetPos - screenCenter;

            // Apply deadzone filtering
            if (rawOffset.Length() < SnapSettings.SnapDeadzone * 1000f)
                return Vector2.Zero;

            // Apply smoothing
            Vector2 smoothed = rawOffset * SnapSettings.SnapStrength;
            smoothed *= 1f - SnapSettings.SnapSmoothing;

            return smoothed;
        }
    }

    public static class PointExtensions
    {
        public static Vector2 ToVector2(this OpenCvSharp.Point p) => new(p.X, p.Y);
    }
}
