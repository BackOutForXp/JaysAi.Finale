// neural v3.0
using JaysAi.Finale.Aim;
using JaysAi.Finale.Input.Handlers;
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.MathHelpers;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Input
{
    public class StickAssist
    {
        private readonly ControllerInputState _state;
        private readonly StickCalibration _calibration;
        private readonly PIDController _pidX;
        private readonly PIDController _pidY;

        private float _sensitivityX = 1.0f;
        private float _sensitivityY = 1.0f;
        private float _deadzone = 0.1f;

        public StickAssist(StickCalibration calibration)
        {
            _state = new ControllerInputState();
            _calibration = calibration;
            _pidX = new PIDController(0.3f, 0.0f, 0.05f);
            _pidY = new PIDController(0.3f, 0.0f, 0.05f);
        }

        public void SetSensitivity(float x, float y)
        {
            _sensitivityX = x;
            _sensitivityY = y;
        }

        public void SetDeadzone(float deadzone)
        {
            _deadzone = Math.Clamp(deadzone, 0f, 1f);
        }

        public void UpdateInput(ControllerInputState input)
        {
            _state.CopyFrom(input);
        }

        public Vector2D CalculateAdjustedStick(Vector2D targetDelta)
        {
            if (targetDelta.Length < _deadzone)
                return Vector2D.Zero;

            float outputX = _pidX.Compute(targetDelta.X) * _sensitivityX;
            float outputY = _pidY.Compute(targetDelta.Y) * _sensitivityY;

            var rawStick = new Vector2D(outputX, outputY);
            return _calibration.ApplyDeadzoneAndNormalize(rawStick);
        }

        public void Reset()
        {
            _pidX.Reset();
            _pidY.Reset();
        }
    }
}
