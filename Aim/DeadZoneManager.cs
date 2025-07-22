//heavenly v3.0
using System;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Aim
{
    public static class DeadZoneManager
    {
        private static double _deadZoneRadius = 0.05; // Default: 5%
        private static bool _enabled = true;

        public static void SetDeadZone(double radius)
        {
            _deadZoneRadius = Math.Clamp(radius, 0.0, 1.0);
            Logger.Debug($"DeadZone radius set to {_deadZoneRadius}");
        }

        public static void Enable(bool state)
        {
            _enabled = state;
            Logger.Debug($"DeadZone enabled: {_enabled}");
        }

        public static bool IsWithinDeadZone(double x, double y)
        {
            if (!_enabled) return false;
            return Math.Sqrt(x * x + y * y) < _deadZoneRadius;
        }

        public static bool IsWithinDeadZone(Vector2 input)
        {
            return IsWithinDeadZone(input.X, input.Y);
        }

        public static double GetRadius() => _deadZoneRadius;
        public static bool IsEnabled() => _enabled;
    }
}
