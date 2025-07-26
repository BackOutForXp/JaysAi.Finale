// Neural v3.0 — KalmanFilter.cs
using System;
using System.Numerics;
using System.Windows.Media.Media3D;

namespace JaysAi.Finale.AI
{
    public class KalmanFilter : IKalmanPredictor
    {
        private Vector3 _statePosition;
        private Vector3 _stateVelocity;

        private Matrix3x3 _errorCovariance;
        private DateTime _lastUpdate;

        private readonly float _processNoise;
        private readonly float _measurementNoise;

        public KalmanFilter(float processNoise = 1e-3f, float measurementNoise = 1e-2f)
        {
            _processNoise = processNoise;
            _measurementNoise = measurementNoise;
            Reset();
        }

        public void Update(Vector3 observedPosition, DateTime timestamp)
        {
            if (_lastUpdate != default)
            {
                float deltaTime = (float)(timestamp - _lastUpdate).TotalSeconds;
                PredictState(deltaTime);
                PredictCovariance(deltaTime);
            }

            // Kalman Gain calculation
            var kalmanGain = _errorCovariance /
                             (_errorCovariance + Matrix3x3.Identity * _measurementNoise);

            // Measurement residual
            Vector3 residual = observedPosition - _statePosition;

            // Update estimates
            _statePosition += kalmanGain * residual;
            _stateVelocity += kalmanGain * (residual / (float)Math.Max((timestamp - _lastUpdate).TotalSeconds, 1e-6));

            // Update error covariance
            _errorCovariance = (Matrix3x3.Identity - kalmanGain) * _errorCovariance;

            _lastUpdate = timestamp;
        }

        public Vector3 Predict(TimeSpan deltaTime)
        {
            float dt = (float)deltaTime.TotalSeconds;
            return _statePosition + _stateVelocity * dt;
        }

        public void Reset()
        {
            _statePosition = Vector3.Zero;
            _stateVelocity = Vector3.Zero;
            _errorCovariance = Matrix3x3.Identity;
            _lastUpdate = default;
        }

        private void PredictState(float dt)
        {
            _statePosition += _stateVelocity * dt;
        }

        private void PredictCovariance(float dt)
        {
            _errorCovariance += Matrix3x3.Identity * _processNoise * dt;
        }
    }
}
