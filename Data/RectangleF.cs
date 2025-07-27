// Neural v3.1
using System;
using System.Numerics;

namespace JaysAi.Finale.Data
{
    public struct RectangleF
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float Right => X + Width;
        public float Bottom => Y + Height;

        public Vector2 TopLeft => new(X, Y);
        public Vector2 BottomRight => new(X + Width, Y + Height);
        public Vector2 Center => new(X + Width / 2, Y + Height / 2);

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Contains(float px, float py)
        {
            return px >= X && px <= Right && py >= Y && py <= Bottom;
        }

        public bool Contains(Vector2 point) => Contains(point.X, point.Y);

        public bool Intersects(RectangleF other)
        {
            return !(Right < other.X || other.Right < X || Bottom < other.Y || other.Bottom < Y);
        }

        public override string ToString() => $"[{X},{Y}] {Width}x{Height}";
    }
}
