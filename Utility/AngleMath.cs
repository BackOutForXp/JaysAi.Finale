//monarch v2.1
using System;
using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public static class AngleMath
    {
        public static float CalculateAngle(Vector2 source, Vector2 target)
        {
            float dx = target.X - source.X;
            float dy = target.Y - source.Y;
            return (float)Math.Atan2(dy, dx) * (180f / (float)Math.PI);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        public static float AngleDelta(float angle1, float angle2)
        {
            float delta = (angle2 - angle1 + 180) % 360 - 180;
            return delta < -180 ? delta + 360 : delta;
        }

        public static bool IsWithinFOV(Vector2 center, Vector2 target, float fov)
        {
            float angleToTarget = CalculateAngle(center, target);
            float delta = AngleDelta(0, angleToTarget); // assuming player view is 0
            return Math.Abs(delta) <= fov / 2f;
        }

        public static float ClampAngle(float angle)
        {
            if (angle > 180f) angle -= 360f;
            if (angle < -180f) angle += 360f;
            return angle;
        }
    }
}
