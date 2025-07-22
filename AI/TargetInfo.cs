//heavenly v3.0
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class TargetInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Rect BoundingBox { get; set; }
        public Point2f Center => new Point2f(BoundingBox.X + BoundingBox.Width / 2f, BoundingBox.Y + BoundingBox.Height / 2f);

        public float Distance { get; set; } // Distance from camera/player
        public Vector2 Velocity { get; set; } // Target motion (for prediction)
        public bool IsVisible { get; set; }
        public bool IsEnemy { get; set; }
        public bool IsTracked { get; set; }

        public float Health { get; set; }
        public float ThreatLevel { get; set; }

        public float ScreenX { get; set; }
        public float ScreenY { get; set; }

        public TargetInfo(int id, Rect bbox)
        {
            Id = id;
            BoundingBox = bbox;
            Velocity = new Vector2(0, 0);
            IsVisible = true;
        }
    }

    public struct Vector2
    {
        public float X;
        public float Y;

        public float Magnitude => (float)System.Math.Sqrt(X * X + Y * Y);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
