// neural v3.0
using JaysAi.Finale.Aim;
using JaysAi.Finale.Input;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Aimbot
{
    public class StickXModule
    {
        private readonly PIDController _xController;
        private readonly PIDController _yController;
        private readonly float _stickDeadzone;

        public StickXModule(float kp = 0.9f, float ki = 0.05f, float kd = 0.025f, float deadzone = 0.05f)
        {
            _xController = new PIDController(kp, ki, kd);
            _yController = new PIDController(kp, ki, kd);
            _stickDeadzone = deadzone;
        }

        public Vector2 CalculateStickAdjustment(Vector2 targetDelta)
        {
            if (targetDelta.Length < _stickDeadzone)
                return Vector2.Zero;

            float adjustX = _xController.Calculate(targetDelta.X);
            float adjustY = _yController.Calculate(targetDelta.Y);

            return new Vector2(adjustX, adjustY);
        }

        public void Reset()
        {
            _xController.Reset();
            _yController.Reset();
        }
    }
}
