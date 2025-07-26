// Neural v3.1 — PredictionHelper.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public static class PredictionHelper
    {
        /// <summary>
        /// Predicts the future position of a moving target based on current velocity and delay.
        /// </summary>
        public static Vector3 PredictFuturePosition(Vector3 currentPosition, Vector3 velocity, float delaySeconds)
        {
            return currentPosition + velocity * delaySeconds;
        }

        /// <summary>
        /// Calculates linear velocity between two positions and time delta.
        /// </summary>
        public static Vector3 CalculateVelocity(Vector3 start, Vector3 end, float deltaTime)
        {
            if (deltaTime <= 0.0001f) return Vector3.Zero;
            return (end - start) / deltaTime;
        }

        /// <summary>
        /// Calculates distance between two 3D points.
        /// </summary>
        public static float Distance(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        /// <summary>
        /// Calculates 2D screen space distance between two vectors.
        /// </summary>
        public static float Distance2D(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        /// <summary>
        /// Determines if a velocity vector is negligible (target is mostly stationary).
        /// </summary>
        public static bool IsNearlyStatic(Vector3 velocity, float threshold = 0.1f)
        {
            return velocity.Length() < threshold;
        }
    }
}
