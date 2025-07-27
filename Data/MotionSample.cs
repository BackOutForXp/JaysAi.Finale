using System;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class MotionSample
    {
        public DateTime Timestamp { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }

        public MotionSample()
        {
            Timestamp = DateTime.UtcNow;
            Position = Vector3.Zero;
            Velocity = Vector3.Zero;
        }

        public MotionSample(Vector3 position, Vector3 velocity)
        {
            Timestamp = DateTime.UtcNow;
            Position = position;
            Velocity = velocity;
        }

        public float TimeSince()
        {
            return (float)(DateTime.UtcNow - Timestamp).TotalSeconds;
        }

        public bool IsStale(float maxAgeSeconds = 0.5f)
        {
            return TimeSince() > maxAgeSeconds;
        }
    }
}
