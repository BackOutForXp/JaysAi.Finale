//heavenly v3.0
using System;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Aim
{
    public static class PredictionAid
    {
        private const float FrameDuration = 1f / 60f; // Assuming 60 FPS target

        public static Vector2 Predict2D(Vector2 currentPos, Vector2 velocity, float latencyMs)
        {
            float predictionTime = (latencyMs / 1000f) + FrameDuration;
            return currentPos + (velocity * predictionTime);
        }

        public static Vector3 Predict3D(Vector3 currentPos, Vector3 velocity, float latencyMs)
        {
            float predictionTime = (latencyMs / 1000f) + FrameDuration;
            return currentPos + (velocity * predictionTime);
        }

        public static float EstimatePingCompensation(float rawPing)
        {
            // Clamp to a safe range and add a buffer for inconsistent pings
            return Math.Clamp(rawPing + 10f, 20f, 100f);
        }

        public static float GetPredictionTime(float latencyMs)
        {
            return (latencyMs / 1000f) + FrameDuration;
        }
    }

    public struct Vector2
    {
        public float X, Y;
        public Vector2(float x, float y) { X = x; Y = y; }

        public static Vector2 operator +(Vector2 a, Vector2 b) =>
            new Vector2(a.X + b.X, a.Y + b.Y);

        public static Vector2 operator *(Vector2 a, float scalar) =>
            new Vector2(a.X * scalar, a.Y * scalar);
    }

    public struct Vector3
    {
        public float X, Y, Z;
        public Vector3(float x, float y, float z) { X = x; Y = y; Z = z; }

        public static Vector3 operator +(Vector3 a, Vector3 b) =>
            new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3 operator *(Vector3 a, float scalar) =>
            new Vector3(a.X * scalar, a.Y * scalar, a.Z * scalar);
    }
}
