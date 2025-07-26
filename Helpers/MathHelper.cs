// Neural v3.0 — MathHelper.cs
using System;

namespace JaysAi.Finale.Helpers
{
    public static class MathHelper
    {
        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        public static int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp(t, 0f, 1f);
        }

        public static double Lerp(double a, double b, double t)
        {
            return a + (b - a) * Clamp(t, 0.0, 1.0);
        }

        public static float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle < 0f)
                angle += 360f;
            return angle;
        }

        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static float Sigmoid(float value)
        {
            return 1f / (1f + (float)Math.Exp(-value));
        }

        public static float SmoothStep(float edge0, float edge1, float x)
        {
            x = Clamp((x - edge0) / (edge1 - edge0), 0f, 1f);
            return x * x * (3f - 2f * x);
        }

        public static bool ApproximatelyEqual(float a, float b, float tolerance = 0.0001f)
        {
            return Math.Abs(a - b) < tolerance;
        }
    }
}
