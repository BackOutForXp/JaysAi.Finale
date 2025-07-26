// neural v3.0
using System;

namespace JaysAi.Finale.Helpers.System
{
    public static class AngleMath
    {
        /// <summary>
        /// Normalizes an angle between -180 and 180 degrees.
        /// </summary>
        public static float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle > 180f) angle -= 360f;
            if (angle < -180f) angle += 360f;
            return angle;
        }

        /// <summary>
        /// Calculates the shortest angular difference between two angles.
        /// </summary>
        public static float DeltaAngle(float from, float to)
        {
            float delta = NormalizeAngle(to - from);
            return delta;
        }

        /// <summary>
        /// Smoothly interpolates between two angles using a factor (0-1).
        /// </summary>
        public static float LerpAngle(float from, float to, float t)
        {
            float delta = DeltaAngle(from, to);
            return from + delta * Clamp01(t);
        }

        /// <summary>
        /// Clamps a value between 0 and 1.
        /// </summary>
        public static float Clamp01(float value)
        {
            if (value < 0f) return 0f;
            if (value > 1f) return 1f;
            return value;
        }

        /// <summary>
        /// Returns true if the angle difference is within a tolerance.
        /// </summary>
        public static bool IsWithinAngle(float from, float to, float tolerance)
        {
            return System.Math.Abs(DeltaAngle(from, to)) <= tolerance;
        }
    }
}
