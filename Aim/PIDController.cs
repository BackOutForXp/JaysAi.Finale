//heavenly v3.0
using System;

namespace JaysAi.Finale.Aim
{
    public class PIDController
    {
        private double _kp;
        private double _ki;
        private double _kd;

        private double _previousError;
        private double _integral;
        private DateTime _lastUpdate;

        public PIDController(double kp = 0.2, double ki = 0.01, double kd = 0.04)
        {
            _kp = kp;
            _ki = ki;
            _kd = kd;
            _lastUpdate = DateTime.Now;
        }

        public double Update(double currentError)
        {
            var now = DateTime.Now;
            var deltaTime = (now - _lastUpdate).TotalSeconds;
            _lastUpdate = now;

            _integral += currentError * deltaTime;
            var derivative = (currentError - _previousError) / deltaTime;
            _previousError = currentError;

            return (_kp * currentError) + (_ki * _integral) + (_kd * derivative);
        }

        public void Reset()
        {
            _previousError = 0;
            _integral = 0;
            _lastUpdate = DateTime.Now;
        }

        public void SetTunings(double kp, double ki, double kd)
        {
            _kp = kp;
            _ki = ki;
            _kd = kd;
        }
    }
}
