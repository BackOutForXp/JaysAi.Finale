// Neural v3.1
using System;
using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public static class AngleHelper
    {
        public static float GetAngleBetween(Vector2 from, Vector2 to)
        {
            var direction = to - from;
            return MathF.Atan2(direction.Y, direction.X) * (180f / MathF.PI);
        }

        public static float GetDistance(Vector2 from, Vector2 to)
        {
            return Vector2.Distance(from, to);
        }

        public static float GetFov(Vector2 from, Vector2 to)
        {
            return GetDistance(from, to); // FOV in screen units (pixels)
        }

        public static bool IsWithinFov(Vector2 from, Vector2 to, float maxFov)
        {
            return GetFov(from, to) <= maxFov;
        }

        public static float NormalizeAngle(float angle)
        {
            while (angle < -180f) angle += 360f;
            while (angle > 180f) angle -= 360f;
            return angle;
        }

        public static float ClampFov(float fov, float min, float max)
        {
            return MathF.Clamp(fov, min, max);
        }
    }
}
