using System;
using System.Numerics;

namespace JaysAi.Finale.Math
{
    public static class Vector2Extensions
    {
        public static float DistanceTo(this Vector2 from, Vector2 to)
        {
            return Vector2.Distance(from, to);
        }

        public static Vector2 ClampMagnitude(this Vector2 vector, float maxLength)
        {
            if (vector.Length() <= maxLength) return vector;
            return Vector2.Normalize(vector) * maxLength;
        }

        public static Vector2 LerpTo(this Vector2 from, Vector2 to, float t)
        {
            return from + (to - from) * t;
        }

        public static bool IsZero(this Vector2 vec)
        {
            return vec == Vector2.Zero;
        }
    }
}
