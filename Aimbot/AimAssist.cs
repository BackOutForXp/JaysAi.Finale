//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aim;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Aimbot
{
    public static class AimAssist
    {
        private static SnapTarget _currentTarget;
        private static float _smoothingFactor = 0.15f;

        public static void Update(Vector2 crosshairPosition)
        {
            if (!FeatureToggleManager.IsEnabled("AimAssist"))
                return;

            var potentialTargets = TargetingSystem.GetVisibleTargets();
            _currentTarget = TargetEvaluator.EvaluateBestTarget(potentialTargets, crosshairPosition, maxSnapDistance: 250f);

            if (_currentTarget != null)
            {
                Vector2 direction = _currentTarget.ScreenPosition - crosshairPosition;
                Vector2 movement = direction * _smoothingFactor;

                InputDispatcher.MoveMouseBy(movement.X, movement.Y);
                Logger.LogDebug($"[AimAssist] Adjusted by {movement}");
            }
        }

        public static void SetSmoothing(float factor)
        {
            _smoothingFactor = Math.Clamp(factor, 0.01f, 1f);
        }
    }
}
