//heavenly v3.0
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class TrackTarget
    {
        public int Id { get; set; }
        public Rect BoundingBox { get; set; }
        public float Confidence { get; set; }
        public bool IsEnemy { get; set; }
        public Point2f Center => new(
            BoundingBox.X + BoundingBox.Width / 2f,
            BoundingBox.Y + BoundingBox.Height / 2f
        );

        public TrackTarget(int id, Rect bbox, float confidence, bool isEnemy)
        {
            Id = id;
            BoundingBox = bbox;
            Confidence = confidence;
            IsEnemy = isEnemy;
        }

        public float GetDistanceTo(Point2f point)
        {
            float dx = point.X - Center.X;
            float dy = point.Y - Center.Y;
            return (float)System.Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            return $"TrackTarget #{Id} | Pos: {Center} | Enemy: {IsEnemy} | Conf: {Confidence:F2}";
        }
    }
}
