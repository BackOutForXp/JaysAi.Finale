// neural v3.0
using System;

namespace JaysAi.Finale.Aim
{
    public class PIDController
    {
        private float kp;
        private float ki;
        private float kd;

        private float previousError;
        private float integral;

        private float outputLimit;

        public PIDController(float p = 1.0f, float i = 0.0f, float d = 0.0f, float outputLimit = 1.0f)
        {
            kp = p;
            ki = i;
            kd = d;
            this.outputLimit = outputLimit;
        }

        public void SetGains(float p, float i, float d)
        {
            kp = p;
            ki = i;
            kd = d;
        }

        public void SetOutputLimit(float limit)
        {
            outputLimit = Math.Clamp(limit, 0f, float.MaxValue);
        }

        public float Update(float error, float deltaTime)
        {
            if (deltaTime <= 0f) return 0f;

            integral += error * deltaTime;
            float derivative = (error - previousError) / deltaTime;

            float output = (kp * error) + (ki * integral) + (kd * derivative);
            previousError = error;

            return Math.Clamp(output, -outputLimit, outputLimit);
        }

        public void Reset()
        {
            previousError = 0f;
            integral = 0f;
        }
    }
}
