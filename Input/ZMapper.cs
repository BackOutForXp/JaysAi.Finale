// neural v3.0
using System;

namespace JaysAi.Finale.Input
{
    public class ZMapper
    {
        private float _sensitivity = 1.0f;
        private float _deadzone = 0.1f;
        private float _exponent = 1.5f;

        public ZMapper(float sensitivity = 1.0f, float deadzone = 0.1f, float exponent = 1.5f)
        {
            _sensitivity = Math.Clamp(sensitivity, 0.1f, 10.0f);
            _deadzone = Math.Clamp(deadzone, 0f, 1f);
            _exponent = Math.Clamp(exponent, 1.0f, 5.0f);
        }

        /// <summary>
        /// Maps a raw stick input (from -1 to 1) through deadzone and exponential scaling.
        /// </summary>
        public float MapInput(float raw)
        {
            if (Math.Abs(raw) < _deadzone)
                return 0f;

            float adjusted = (Math.Abs(raw) - _deadzone) / (1 - _deadzone);
            float curved = (float)Math.Pow(adjusted, _exponent);
            float result = curved * Math.Sign(raw) * _sensitivity;

            return Math.Clamp(result, -1f, 1f);
        }

        public void UpdateSensitivity(float newSensitivity)
        {
            _sensitivity = Math.Clamp(newSensitivity, 0.1f, 10.0f);
        }

        public void UpdateDeadzone(float newDeadzone)
        {
            _deadzone = Math.Clamp(newDeadzone, 0f, 1f);
        }

        public void UpdateExponent(float newExponent)
        {
            _exponent = Math.Clamp(newExponent, 1.0f, 5.0f);
        }

        public float Sensitivity => _sensitivity;
        public float Deadzone => _deadzone;
        public float Exponent => _exponent;
    }
}
