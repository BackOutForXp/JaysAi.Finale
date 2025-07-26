// neural v3.0
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
            return velocity.LengthSquared() < threshold * threshold;
        }

        /// <summary>
        /// Clamps a float value within a specified min and max range.
        /// </summary>
        public static float Clamp(float value, float min, float max)
        {
            return MathF.Max(min, MathF.Min(max, value));
        }

        /// <summary>
        /// Predicts time to reach a target based on distance and projectile speed.
        /// </summary>
        public static float CalculateFlightTime(float distance, float projectileSpeed)
        {
            return projectileSpeed > 0f ? distance / projectileSpeed : 0f;
        }

        /// <summary>
        /// Predicts target position with bullet travel time and movement velocity.
        /// </summary>
        public static Vector3 PredictBulletLead(Vector3 targetPos, Vector3 targetVelocity, float distance, float projectileSpeed)
        {
            float flightTime = CalculateFlightTime(distance, projectileSpeed);
            return PredictFuturePosition(targetPos, targetVelocity, flightTime);
        }
    }
}
