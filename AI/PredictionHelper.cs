//heavenly v3.0
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public static class PredictionHelper
    {
        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp(t, 0f, 1f);
        }

        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public static float AngleBetween(Vector2 from, Vector2 to)
        {
            float dx = to.X - from.X;
            float dy = to.Y - from.Y;
            return MathF.Atan2(dy, dx) * (180f / MathF.PI);
        }

        public static Vector2 SmoothPredict(Vector2 previous, Vector2 current, float smoothing)
        {
            return new Vector2(
                Lerp(previous.X, current.X, smoothing),
                Lerp(previous.Y, current.Y, smoothing)
            );
        }

        public static Vector2 Average(IList<Vector2> samples)
        {
            if (samples == null || samples.Count == 0)
                return new Vector2(0, 0);

            float sumX = 0, sumY = 0;
            foreach (var sample in samples)
            {
                sumX += sample.X;
                sumY += sample.Y;
            }

            return new Vector2(sumX / samples.Count, sumY / samples.Count);
        }
    }

    public struct Vector2
    {
        public float X, Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X:F2}, {Y:F2})";
    }
}
