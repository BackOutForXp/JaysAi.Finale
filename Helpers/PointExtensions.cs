// Neural v3.0 — VectorHelper.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.Helpers
{
    public static class VectorHelper
    {
        public static float Distance2D(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        public static float Distance3D(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        public static Vector2 Normalize(Vector2 v)
        {
            return v == Vector2.Zero ? v : Vector2.Normalize(v);
        }

        public static Vector3 Normalize(Vector3 v)
        {
            return v == Vector3.Zero ? v : Vector3.Normalize(v);
        }

        public static float Magnitude(Vector2 v)
        {
            return v.Length();
        }

        public static float Magnitude(Vector3 v)
        {
            return v.Length();
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return Vector3.Dot(a, b);
        }

        public static float AngleBetween(Vector3 a, Vector3 b)
        {
            var dot = Vector3.Dot(a, b);
            var magA = a.Length();
            var magB = b.Length();
            return (float)Math.Acos(dot / (magA * magB)) * (180f / (float)Math.PI);
        }

        public static Vector2 ProjectTo2D(Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }
    }
}
