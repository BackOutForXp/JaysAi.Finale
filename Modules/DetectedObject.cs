// Monarch v1.0 – DetectedObject.cs
// ✅ Monarch Fix Checklist
// [x] Includes velocity, center, and team checks
// [x] Safe structure for YOLO/ESP fusion
// [x] Designed for modular upgrades

using OpenCvSharp;
using System;

namespace JaysAi.Finale.Modules
{
    public class DetectedObject
    {
        public Rect BoundingBox { get; set; }
        public Point2f Center2D => new(
            BoundingBox.X + BoundingBox.Width / 2f,
            BoundingBox.Y + BoundingBox.Height / 2f);

        public Point2f Velocity { get; set; } = new(0, 0);
        public DateTime LastSeenTime { get; set; } = DateTime.Now;
        public bool IsVisible { get; set; } = true;

        public int ObjectID { get; set; } = -1;
        public string Label { get; set; } = "";
        public float Confidence { get; set; } = 0.0f;

        public bool IsEnemy { get; set; } = true;
        public bool IsTracked { get; set; } = false;

        public void UpdateVelocity(Point2f previousCenter)
        {
            var currentTime = DateTime.Now;
            var timeDiff = (float)(currentTime - LastSeenTime).TotalSeconds;
            if (timeDiff > 0)
            {
                Velocity = new Point2f(
                    (Center2D.X - previousCenter.X) / timeDiff,
                    (Center2D.Y - previousCenter.Y) / timeDiff
                );
            }
            LastSeenTime = currentTime;
        }
    }
}
