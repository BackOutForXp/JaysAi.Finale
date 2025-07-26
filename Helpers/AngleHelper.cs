// Neural v3.0 — AngleHelper.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.Helpers
{
    public static class AngleHelper
    {
        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        public static float ToDegrees(float radians)
        {
            return radians * (180f / MathF.PI);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        public static float ToRadians(float degrees)
        {
            return degrees * (MathF.PI / 180f);
        }

        /// <summary>
        /// Normalizes an angle to the range [-180, 180].
        /// </summary>
        public static float NormalizeAngle(float angle)
        {
            while (angle > 180f) angle -= 360f;
            while (angle < -180f) angle += 360f;
            return angle;
        }

        /// <summary>
        /// Calculates the angle between two 2D vectors in degrees.
        /// </summary>
        public static float AngleBetween(Vector2 from, Vector2 to)
        {
            Vector2 direction = Vector2.Normalize(to - from);
            float angle = MathF.Atan2(direction.Y, direction.X);
            return ToDegrees(angle);
        }

        /// <summary>
        /// Calculates the smallest difference between two angles.
        /// </summary>
        public static float DeltaAngle(float current, float target)
        {
            float delta = NormalizeAngle(target - current);
            return delta;
        }

        /// <summary>
        /// Rotates an angle toward another by maxDelta degrees.
        /// </summary>
        public static float RotateTowards(float current, float target, float maxDelta)
        {
            float delta = DeltaAngle(current, target);
            if (MathF.Abs(delta) <= maxDelta)
                return target;

            return current + MathF.Sign(delta) * maxDelta;
        }
    }
}
