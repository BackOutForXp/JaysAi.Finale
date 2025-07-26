// Neural v3.0 — IKalmanPredictor.cs
using System;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    /// <summary>
    /// Interface for implementing Kalman filter–based prediction.
    /// </summary>
    public interface IKalmanPredictor
    {
        /// <summary>
        /// Updates the Kalman filter with a new observation.
        /// </summary>
        /// <param name="observedPosition">The observed 3D position of the target.</param>
        /// <param name="timestamp">Timestamp of the observation.</param>
        void Update(Vector3 observedPosition, DateTime timestamp);

        /// <summary>
        /// Predicts the future position of the target after a given time delta.
        /// </summary>
        /// <param name="deltaTime">The amount of time into the future to predict.</param>
        /// <returns>Predicted 3D position of the target.</returns>
        Vector3 Predict(TimeSpan deltaTime);

        /// <summary>
        /// Resets the internal Kalman filter state.
        /// </summary>
        void Reset();
    }
}
