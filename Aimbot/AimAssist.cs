//monarch v2.1 – Aim Assist Logic Engine
using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public static class AimAssist
    {
        private static float _aimSmoothness = 0.75f;
        private static float _deadzone = 2.0f;

        public static void Apply(DetectionObject target)
        {
            if (target == null) return;

            var screenCenterX = ScreenHelper.CenterX;
            var screenCenterY = ScreenHelper.CenterY;

            var deltaX = (target.X + target.Width / 2) - screenCenterX;
            var deltaY = (target.Y + target.Height / 2) - screenCenterY;

            if (Math.Abs(deltaX) < _deadzone && Math.Abs(deltaY) < _deadzone)
                return;

            var adjustedX = deltaX * _aimSmoothness;
            var adjustedY = deltaY * _aimSmoothness;

            MouseMover.MoveBy(adjustedX, adjustedY);
        }

        public static void SetSmoothness(float smoothness)
        {
            _aimSmoothness = Math.Clamp(smoothness, 0.1f, 1.5f);
        }

        public static void SetDeadzone(float deadzone)
        {
            _deadzone = Math.Clamp(deadzone, 0f, 10f);
        }
    }
}
