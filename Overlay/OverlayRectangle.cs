// Neural v3.0 — OverlayRectangle.cs
using System;

namespace JaysAi.Finale.Overlay
{
    public readonly struct OverlayRectangle
    {
        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }

        public float Right => X + Width;
        public float Bottom => Y + Height;
        public float CenterX => X + Width / 2f;
        public float CenterY => Y + Height / 2f;

        public OverlayRectangle(float x, float y, float width, float height)
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

        public bool Intersects(OverlayRectangle other)
        {
            return X < other.Right &&
                   Right > other.X &&
                   Y < other.Bottom &&
                   Bottom > other.Y;
        }

        public OverlayRectangle Inflate(float padding)
        {
            return new OverlayRectangle(
                X - padding,
                Y - padding,
                Width + (padding * 2),
                Height + (padding * 2)
            );
        }

        public override string ToString()
        {
            return $"[X:{X}, Y:{Y}, W:{Width}, H:{Height}]";
        }
    }
}
