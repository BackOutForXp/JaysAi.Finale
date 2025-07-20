//monarch v2.0
using System;
using System.Numerics;

namespace JaysAi.Finale.Aim
{
    public static class PIDController
    {
        private static Vector2 integral = Vector2.Zero;
        private static Vector2 previousError = Vector2.Zero;

        public static float Kp = 0.6f;  // Proportional gain
        public static float Ki = 0.05f; // Integral gain
        public static float Kd = 0.2f;  // Derivative gain

        public static Vector2 Calculate(Vector2 currentError)
        {
            // Integrate the error over time
            integral += currentError;

            // Derivative of the error
            Vector2 derivative = currentError - previousError;

            // PID formula
            Vector2 output =
                currentError * Kp +
                integral * Ki +
                derivative * Kd;

            previousError = currentError;

            return output;
        }

        public static void Reset()
        {
            integral = Vector2.Zero;
            previousError = Vector2.Zero;
        }
    }
}
