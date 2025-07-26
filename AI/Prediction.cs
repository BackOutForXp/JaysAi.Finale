// neural v3.0
using System;
using System.Numerics;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public static class Prediction
    {
        /// <summary>
        /// Predicts a future position based on current velocity and deltaTime.
        /// </summary>
        public static Vector2 Predict2D(Vector2 currentPosition, Vector2 velocity, float deltaTime)
        {
            return currentPosition + (velocity * deltaTime);
        }

        /// <summary>
        /// Predicts future position using velocity + acceleration for 2D.
        /// </summary>
        public static Vector2 Predict2DAdvanced(Vector2 currentPosition, Vector2 velocity, Vector2 acceleration, float deltaTime)
        {
            return currentPosition + (velocity * deltaTime) + (0.5f * acceleration * deltaTime * deltaTime);
        }

        /// <summary>
        /// Predicts future position using known history samples.
        /// </summary>
        public static Vector2 PredictFromMotionStats(Vector2 currentPosition, MotionStats motionStats, float deltaTime)
        {
            if (motionStats.SampleCount < 2)
                return currentPosition;

            return Predict2DAdvanced(currentPosition, motionStats.Velocity, motionStats.Acceleration, deltaTime);
        }

        /// <summary>
        /// Applies prediction for 3D space (XYZ) with optional vertical prediction.
        /// </summary>
        public static Vector3 Predict3D(Vector3 currentPosition, Vector3 velocity, float deltaTime, bool includeZ = true)
        {
            var delta = velocity * deltaTime;
            return includeZ ? currentPosition + delta : new Vector3(currentPosition.X + delta.X, currentPosition.Y + delta.Y, currentPosition.Z);
        }

        /// <summary>
        /// Calculates linear velocity from two 2D positions and time delta.
        /// </summary>
        public static Vector2 CalculateVelocity(Vector2 previous, Vector2 current, float deltaTime)
        {
            if (deltaTime <= 0f) return Vector2.Zero;
            return (current - previous) / deltaTime;
        }

        /// <summary>
        /// Calculates acceleration from two velocities and deltaTime.
        /// </summary>
        public static Vector2 CalculateAcceleration(Vector2 velocity1, Vector2 velocity2, float deltaTime)
        {
            if (deltaTime <= 0f) return Vector2.Zero;
            return (velocity2 - velocity1) / deltaTime;
        }
    }
}
