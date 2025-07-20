// File: AI/MonarchAimAI.cs
using System;
using System.Numerics;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Core;

namespace JaysAi.Finale.AI
{
    public class MonarchAimAI
    {
        private readonly AppSettings _settings;

        public MonarchAimAI(AppSettings settings)
        {
            _settings = settings;
        }

        public Vector2 GetSmoothedAim(Vector2 current, Vector2 target)
        {
            float smoothing = _settings.SmoothingAmount;
            if (smoothing <= 0f) return target;

            Vector2 delta = target - current;
            return current + delta / smoothing;
        }

        public bool IsWithinFov(Vector2 current, Vector2 target)
        {
            float maxFov = _settings.FovLimit;
            return Vector2.Distance(current, target) <= maxFov;
        }

        public Vector2 ClampFov(Vector2 current, Vector2 target)
        {
            if (!IsWithinFov(current, target))
                return current;

            return target;
        }
    }
}
