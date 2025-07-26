// neural v3.0
using System;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public class StickCalibration
    {
        private const float DeadzoneDefault = 0.05f;
        private const float RangeDefault = 1.0f;

        public Vector2 Center { get; private set; } = Vector2.Zero;
        public float Deadzone { get; private set; } = DeadzoneDefault;
        public float MaxRange { get; private set; } = RangeDefault;

        public void Calibrate(Vector2 sample)
        {
            Center = sample;
        }

        public void SetDeadzone(float dz)
        {
            Deadzone = Math.Clamp(dz, 0f, 1f);
        }

        public void SetMaxRange(float range)
        {
            MaxRange = Math.Clamp(range, 0.1f, 1f);
        }

        public Vector2 NormalizeInput(Vector2 raw)
        {
            var offset = raw - Center;
            var magnitude = offset.Length();

            if (magnitude < Deadzone)
                return Vector2.Zero;

            return Vector2.Normalize(offset) * MathF.Min((magnitude - Deadzone) / (MaxRange - Deadzone), 1f);
        }

        public bool IsOutsideDeadzone(Vector2 raw)
        {
            return (raw - Center).LengthSquared() > Deadzone * Deadzone;
        }
    }
}
