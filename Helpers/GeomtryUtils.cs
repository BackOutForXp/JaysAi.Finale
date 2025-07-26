// Neural v3.0 — GeometryUtils.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.Helpers
{
    public static class GeometryUtils
    {
        public static float Distance2D(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        public static float Distance3D(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        public static float AngleBetweenVectors(Vector2 from, Vector2 to)
        {
            float dot = Vector2.Dot(Vector2.Normalize(from), Vector2.Normalize(to));
            return MathF.Acos(Math.Clamp(dot, -1f, 1f)) * (180f / MathF.PI);
        }

        public static Vector2 ClampToCircle(Vector2 position, float radius)
        {
            float magnitude = position.Length();
            return magnitude > radius ? Vector2.Normalize(position) * radius : position;
        }

        public static Vector2 RotatePoint(Vector2 point, float angleInDegrees)
        {
            float radians = MathF.PI * angleInDegrees / 180f;
            float cos = MathF.Cos(radians);
            float sin = MathF.Sin(radians);
            return new Vector2(
                cos * point.X - sin * point.Y,
                sin * point.X + cos * point.Y
            );
        }

        public static float NormalizeAngle(float angle)
        {
            while (angle < -180f) angle += 360f;
            while (angle > 180f) angle -= 360f;
            return angle;
        }

        public static bool IsWithinFOV(Vector2 playerDir, Vector2 targetDir, float maxFovDegrees)
        {
            float angle = AngleBetweenVectors(playerDir, targetDir);
            return angle <= maxFovDegrees;
        }
    }
}
