//monarch v2.1 – StickXModule
using System;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Input
{
    public class StickXModule
    {
        private const float SmoothingFactor = 0.1f;
        private const float Deadzone = 0.03f;
        private const float MaxStickValue = 1.0f;

        private float lastX;
        private float lastY;

        public float OutputX { get; private set; }
        public float OutputY { get; private set; }

        public void ApplyInput(float deltaX, float deltaY)
        {
            float smoothedX = SmoothInput(deltaX);
            float smoothedY = SmoothInput(deltaY);

            OutputX = ClampStick(smoothedX);
            OutputY = ClampStick(smoothedY);

            lastX = OutputX;
            lastY = OutputY;
        }

        private float SmoothInput(float input)
        {
            return input * SmoothingFactor;
        }

        private float ClampStick(float value)
        {
            if (Math.Abs(value) < Deadzone)
                return 0;

            return Math.Clamp(value, -MaxStickValue, MaxStickValue);
        }

        public void Reset()
        {
            OutputX = 0;
            OutputY = 0;
            lastX = 0;
            lastY = 0;
        }

        public void ApplyPrediction(PredictionEngine.PredictionData prediction)
        {
            ApplyInput(prediction.VelocityX * 0.65f, prediction.VelocityY * 0.65f);
        }

        public (float X, float Y) GetStickOutput()
        {
            return (OutputX, OutputY);
        }
    }
}
