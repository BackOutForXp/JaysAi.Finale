//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aim;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Aimbot
{
    public static class SnapController
    {
        private static DateTime _lastSnapTime = DateTime.MinValue;

        public static void ExecuteSnap(TargetInfo target)
        {
            if (!SnapConfig.Enabled || target == null || !target.IsVisible)
                return;

            // Respect cooldown
            if ((DateTime.Now - _lastSnapTime).TotalMilliseconds < SnapConfig.SnapCooldownMs)
                return;

            var screenCenter = ScreenManager.GetScreenCenter();
            var targetPoint = new System.Windows.Point(target.ScreenX, target.ScreenY);

            double distance = DistanceHelper.Calculate(screenCenter, targetPoint);
            if (distance > SnapConfig.SnapRadius)
                return;

            if (SnapConfig.RequireVisibility && !LineOfSightChecker.HasLineOfSight(screenCenter, targetPoint))
                return;

            // Apply prediction if enabled
            if (SnapConfig.PredictMovement)
                targetPoint = PredictionAid.AdjustForVelocity(target);

            // Smooth aim toward target
            CursorMover.MoveToward(targetPoint, SnapConfig.SnapStrength);

            _lastSnapTime = DateTime.Now;
        }
    }
}
