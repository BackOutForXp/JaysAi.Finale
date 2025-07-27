// Neural v3.1
using System.Numerics;

namespace JaysAi.Finale.Models
{
    public struct ScreenBox
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Vector2 TopLeft => new(X, Y);
        public Vector2 BottomRight => new(X + Width, Y + Height);

        public bool Contains(Vector2 point) =>
            point.X >= X && point.X <= X + Width &&
            point.Y >= Y && point.Y <= Y + Height;
    }
}
