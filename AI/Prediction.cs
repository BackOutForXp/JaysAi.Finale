//heavenly v3.0 – Linear and Accelerated Prediction Core
using System;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public static class Prediction
    {
        /// <summary>
        /// Predicts future position of a target using constant velocity model.
        /// </summary>
        /// <param name="currentPos">Current position of the target.</param>
        /// <param name="velocity">Current velocity of the target.</param>
        /// <param name="latencyMs">Total prediction time in milliseconds.</param>
        /// <returns>Predicted future position as Vector2.</returns>
        public static Vector2 PredictLinear(Vector2 currentPos, Vector2 velocity, float latencyMs)
        {
            float latencySeconds = latencyMs / 1000f;
            return currentPos + velocity * latencySeconds;
        }

        /// <summary>
        /// Predicts position accounting for acceleration (e.g., changes in velocity).
        /// </summary>
        /// <param name="currentPos">Current position.</param>
        /// <param name="velocity">Current velocity.</param>
        /// <param name="acceleration">Acceleration vector.</param>
        /// <param name="latencyMs">Time to predict into future (ms).</param>
        /// <returns>Future predicted position.</returns>
        public static Vector2 PredictAccelerated(Vector2 currentPos, Vector2 velocity, Vector2 acceleration, float latencyMs)
        {
            float t = latencyMs / 1000f;
            return currentPos + velocity * t + 0.5f * acceleration * t * t;
        }

        /// <summary>
        /// Calculates estimated velocity from two points in time.
        /// </summary>
        public static Vector2 EstimateVelocity(Vector2 prevPos, Vector2 currentPos, float deltaTimeSeconds)
        {
            if (deltaTimeSeconds <= 0.0001f)
                return Vector2.Zero;

            return (currentPos - prevPos) / deltaTimeSeconds;
        }

        /// <summary>
        /// Calculates acceleration from two velocity samples.
        /// </summary>
        public static Vector2 EstimateAcceleration(Vector2 prevVelocity, Vector2 currentVelocity, float deltaTimeSeconds)
        {
            if (deltaTimeSeconds <= 0.0001f)
                return Vector2.Zero;

            return (currentVelocity - prevVelocity) / deltaTimeSeconds;
        }
    }
}
