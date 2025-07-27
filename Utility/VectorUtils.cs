// Neural v3.1 — VectorUtils.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public static class VectorUtils
    {
        public static float Distance(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Math.Clamp(value.X, min.X, max.X),
                Math.Clamp(value.Y, min.Y, max.Y)
            );
        }

        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Math.Clamp(value.X, min.X, max.X),
                Math.Clamp(value.Y, min.Y, max.Y),
                Math.Clamp(value.Z, min.Z, max.Z)
            );
        }

        public static Vector3 Lerp(Vector3 start, Vector3 end, float t)
        {
            return Vector3.Lerp(start, end, t);
        }

        public static Vector2 Lerp(Vector2 start, Vector2 end, float t)
        {
            return Vector2.Lerp(start, end, t);
        }

        public static Vector2 NormalizeSafe(Vector2 v)
        {
            return v == Vector2.Zero ? v : Vector2.Normalize(v);
        }

        public static Vector3 NormalizeSafe(Vector3 v)
        {
            return v == Vector3.Zero ? v : Vector3.Normalize(v);
        }
    }
}
