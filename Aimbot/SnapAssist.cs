//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Config;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Utility;
using System;
using System.Numerics;

namespace JaysAi.Finale.Aimbot
{
    public class SnapAssist
    {
        private static TargetInfo? _currentTarget;
        private static DateTime _lastSnapTime;
        private static bool _isSnapping;

        public static bool IsSnapping => _isSnapping;

        public static void Update()
        {
            if (!SnapConfig.Enabled || !InputManager.IsAiming)
            {
                _isSnapping = false;
                _currentTarget = null;
                return;
            }

            var target = TargetSelector.SelectBestTarget();

            if (target == null)
            {
                _isSnapping = false;
                return;
            }

            _currentTarget = target;
            SnapToTarget(target);
        }

        private static void SnapToTarget(TargetInfo target)
        {
            Vector2 screenTargetPos = ScreenUtils.WorldToScreen(target.Position3D);

            if (!ScreenUtils.IsOnScreen(screenTargetPos))
            {
                Logger.LogDebug("[SnapAssist] Target off-screen, skipping snap.");
                return;
            }

            Vector2 aimOffset = screenTargetPos - InputManager.CurrentAimPosition;

            if (aimOffset.Length() < SnapConfig.SnapRadius)
            {
                InputEmulator.MoveAimBy(aimOffset * SnapConfig.SnapStrength);
                _isSnapping = true;
                _lastSnapTime = DateTime.UtcNow;

                Logger.LogDebug($"[SnapAssist] Snapped to target: {target.Id} at {target.Position3D}");
            }
            else
            {
                _isSnapping = false;
            }
        }
    }
}
