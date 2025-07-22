//heavenly v3.0 – Aim Path Prediction Logic
using System;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public static class AimPathPredicator
    {
        /// <summary>
        /// Predicts where the target will be after a delay (in seconds)
        /// </summary>
        /// <param name="currentPosition">The current 2D position of the target</param>
        /// <param name="velocity">The current 2D velocity vector of the target</param>
        /// <param name="latencyCompensation">Optional latency delay in seconds</param>
        /// <returns>Predicted position</returns>
        public static Vector2 Predict(Vector2 currentPosition, Vector2 velocity, float latencyCompensation = 0.05f)
        {
            return currentPosition + velocity * latencyCompensation;
        }

        /// <summary>
        /// Performs curved prediction using a quadratic approximation
        /// </summary>
        /// <param name="current">Current position</param>
        /// <param name="previous">Previous position</param>
        /// <param name="older">Older position</param>
        /// <param name="latencyCompensation">Delay in seconds</param>
        /// <returns>Predicted position using acceleration curve</returns>
        public static Vector2 PredictCurved(Vector2 current, Vector2 previous, Vector2 older, float latencyCompensation = 0.05f)
        {
            var v1 = current - previous;
            var v2 = previous - older;
            var acceleration = v1 - v2;
            var predictedVelocity = v1 + acceleration * latencyCompensation;
            return current + predictedVelocity * latencyCompensation;
        }

        /// <summary>
        /// Checks if the target is slowing down to adjust aim
        /// </summary>
        public static bool IsTargetDecelerating(Vector2 velocityNow, Vector2 velocityBefore)
        {
            return velocityNow.LengthSquared() < velocityBefore.LengthSquared();
        }
    }
}
