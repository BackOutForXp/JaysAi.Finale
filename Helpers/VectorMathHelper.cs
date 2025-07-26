// neural v3.0
using System;
using System.Numerics;

namespace JaysAi.Finale.Helpers
{
    public static class VectorMathHelper
    {
        public static float Distance(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        public static Vector2 Normalize(Vector2 vector)
        {
            return Vector2.Normalize(vector);
        }

        public static Vector3 Normalize(Vector3 vector)
        {
            return Vector3.Normalize(vector);
        }

        public static float AngleBetween(Vector2 from, Vector2 to)
        {
            float dot = Vector2.Dot(Vector2.Normalize(from), Vector2.Normalize(to));
            return MathF.Acos(Math.Clamp(dot, -1f, 1f)) * (180f / MathF.PI);
        }

        public static float AngleBetween(Vector3 from, Vector3 to)
        {
            float dot = Vector3.Dot(Vector3.Normalize(from), Vector3.Normalize(to));
            return MathF.Acos(Math.Clamp(dot, -1f, 1f)) * (180f / MathF.PI);
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            if (vector.LengthSquared() > maxLength * maxLength)
            {
                return Vector2.Normalize(vector) * maxLength;
            }
            return vector;
        }

        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            if (vector.LengthSquared() > maxLength * maxLength)
            {
                return Vector3.Normalize(vector) * maxLength;
            }
            return vector;
        }

        public static Vector2 Lerp(Vector2 start, Vector2 end, float t)
        {
            return Vector2.Lerp(start, end, t);
        }

        public static Vector3 Lerp(Vector3 start, Vector3 end, float t)
        {
            return Vector3.Lerp(start, end, t);
        }
    }
}
