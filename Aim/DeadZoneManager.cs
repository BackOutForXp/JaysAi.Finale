// neural v3.0
using System;

namespace JaysAi.Finale.Aim
{
    public class DeadZoneManager
    {
        private float deadZoneRadius;

        public DeadZoneManager(float radius = 0.05f)
        {
            SetDeadZone(radius);
        }

        public void SetDeadZone(float radius)
        {
            deadZoneRadius = Math.Clamp(radius, 0f, 1f);
        }

        public bool IsInDeadZone(float x, float y)
        {
            float magnitude = MathF.Sqrt(x * x + y * y);
            return magnitude < deadZoneRadius;
        }

        public bool IsInDeadZone(Vector2 aimInput) => IsInDeadZone(aimInput.X, aimInput.Y);

        public float GetDeadZoneRadius() => deadZoneRadius;
    }

    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
