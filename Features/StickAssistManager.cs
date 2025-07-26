// neural v3.0
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Features
{
    public class StickAssistManager
    {
        private readonly StickInputSimulator stickInput = new();
        private float horizontalOffset = 0f;
        private float verticalOffset = 0f;
        private DateTime lastUpdateTime = DateTime.UtcNow;

        public void Update(float targetDeltaX, float targetDeltaY)
        {
            if (!UserSettings.Current.StickAssistEnabled)
                return;

            TimeSpan deltaTime = DateTime.UtcNow - lastUpdateTime;
            lastUpdateTime = DateTime.UtcNow;

            float multiplier = UserSettings.Current.StickAssistSensitivity;
            float smoothing = UserSettings.Current.StickAssistSmoothing;

            horizontalOffset = Lerp(horizontalOffset, targetDeltaX * multiplier, smoothing);
            verticalOffset = Lerp(verticalOffset, targetDeltaY * multiplier, smoothing);

            stickInput.ApplyMovement(horizontalOffset, verticalOffset);
        }

        public void Reset()
        {
            horizontalOffset = 0f;
            verticalOffset = 0f;
            stickInput.Reset();
        }

        private float Lerp(float from, float to, float t)
        {
            return from + (to - from) * Clamp(t, 0f, 1f);
        }

        private float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
}
