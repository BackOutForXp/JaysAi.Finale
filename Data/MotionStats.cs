// Neural v3.1 — MotionStats.cs
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public struct MotionStats
    {
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public int SampleCount;

        public bool IsValid => SampleCount > 2 && Velocity.LengthSquared() > 0.0001f;
    }
}
