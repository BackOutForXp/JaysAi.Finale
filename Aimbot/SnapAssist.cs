//monarch v2.1 – Smart Snap Aim Module
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Visuals;
using System;

namespace JaysAi.Finale.Aimbot
{
    public static class SnapAssist
    {
        private static float snapStrength = 0.45f; // 0.0 to 1.0
        private static float deadzone = 5.0f;

        public static void LockOn(DetectedObject target)
        {
            var screenCenterX = ScreenManager.Width / 2f;
            var screenCenterY = ScreenManager.Height / 2f;

            float targetCenterX = target.X + (target.Width / 2f);
            float targetCenterY = target.Y + (target.Height / 2f);

            float deltaX = targetCenterX - screenCenterX;
            float deltaY = targetCenterY - screenCenterY;

            if (Math.Abs(deltaX) < deadzone && Math.Abs(deltaY) < deadzone)
                return;

            float moveX = deltaX * snapStrength;
            float moveY = deltaY * snapStrength;

            InputInjector.MoveMouseRelative((int)moveX, (int)moveY);

            AiOverlay.QueueCircle(targetCenterX, targetCenterY, 10, "LOCK", OverlayColor.Yellow);
        }

        public static void SetSnapStrength(float strength)
        {
            snapStrength = Math.Clamp(strength, 0f, 1f);
        }

        public static void SetDeadzone(float zone)
        {
            deadzone = Math.Max(0f, zone);
        }
    }
}
