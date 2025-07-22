//heavenly v3.0
using OpenCvSharp;
using System;

namespace JaysAi.Finale.AI
{
    public class TrackedTarget
    {
        public int Id { get; set; }
        public Rect BoundingBox { get; set; }
        public float Confidence { get; set; }
        public float ThreatLevel { get; set; }
        public Point2f LastKnownCenter { get; private set; }
        public DateTime LastSeen { get; private set; }
        public bool IsEnemy { get; set; }

        public TrackedTarget(int id, Rect bbox, float confidence, bool isEnemy)
        {
            Id = id;
            BoundingBox = bbox;
            Confidence = confidence;
            IsEnemy = isEnemy;
            Update(bbox);
        }

        public void Update(Rect newBox)
        {
            BoundingBox = newBox;
            LastKnownCenter = new Point2f(
                BoundingBox.X + BoundingBox.Width / 2f,
                BoundingBox.Y + BoundingBox.Height / 2f
            );
            LastSeen = DateTime.UtcNow;
        }

        public bool IsExpired(TimeSpan timeout)
        {
            return DateTime.UtcNow - LastSeen > timeout;
        }

        public float DistanceTo(Point2f point)
        {
            return (float)Math.Sqrt(
                Math.Pow(point.X - LastKnownCenter.X, 2) +
                Math.Pow(point.Y - LastKnownCenter.Y, 2)
            );
        }

        public override string ToString()
        {
            return $"TrackedTarget #{Id} | Pos: {LastKnownCenter} | Enemy: {IsEnemy}";
        }
    }
}
