// File: Utility\DrawingUtils.cs

using System;
using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public static class DrawingUtils
    {
        public static float DegreesBetween(Vector2 a, Vector2 b)
        {
            float dot = Vector2.Dot(Vector2.Normalize(a), Vector2.Normalize(b));
            return MathF.Acos(Math.Clamp(dot, -1f, 1f)) * (180f / MathF.PI);
        }

        public static float Distance2D(Vector2 a, Vector2 b)
        {
            return (a - b).Length();
        }

        public static float Distance3D(Vector3 a, Vector3 b)
        {
            return (a - b).Length();
        }

        public static Vector2 ClampToBounds(Vector2 point, float width, float height)
        {
            float x = Math.Clamp(point.X, 0, width);
            float y = Math.Clamp(point.Y, 0, height);
            return new Vector2(x, y);
        }

        public static Vector3 ClampToBounds(Vector3 point, float width, float height, float depth)
        {
            float x = Math.Clamp(point.X, 0, width);
            float y = Math.Clamp(point.Y, 0, height);
            float z = Math.Clamp(point.Z, 0, depth);
            return new Vector3(x, y, z);
        }
    }
}
