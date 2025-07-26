// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Aimbot
{
    public static class SnapLogic
    {
        public static Vector2 CalculateSnapDelta(TargetInfo target, SnapConfig config)
        {
            if (target == null || !target.IsAlive)
                return Vector2.Zero;

            var targetScreenPos = ViewpointTranslator.WorldToScreen(target.Position);
            if (!targetScreenPos.IsValid)
                return Vector2.Zero;

            var screenCenter = ScreenUtils.GetScreenCenter();
            var delta = new Vector2(
                targetScreenPos.X - screenCenter.X,
                targetScreenPos.Y - screenCenter.Y
            );

            return ApplySmoothing(delta, config.SmoothingFactor) * config.Sensitivity;
        }

        private static Vector2 ApplySmoothing(Vector2 delta, float smoothing)
        {
            if (smoothing <= 0) return delta;

            return new Vector2(
                delta.X / smoothing,
                delta.Y / smoothing
            );
        }

        public static bool IsWithinSnapFOV(TargetInfo target, float fov)
        {
            var screenPos = ViewpointTranslator.WorldToScreen(target.Position);
            if (!screenPos.IsValid) return false;

            var center = ScreenUtils.GetScreenCenter();
            var distance = VectorMathHelper.Distance(center, screenPos);
            return distance <= fov;
        }
    }
}
