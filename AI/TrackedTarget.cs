//monarch v2.1 – Visual Detection Target Container
using System;

namespace JaysAi.Finale.Visuals
{
    public class TrackedTarget
    {
        public int Id { get; set; }
        public float X { get; set; }        // Center X on screen
        public float Y { get; set; }        // Center Y on screen
        public float Width { get; set; }
        public float Height { get; set; }

        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        public bool IsVisible { get; set; }
        public bool IsEnemy { get; set; }

        public bool IsValid =>
            IsVisible &&
            IsEnemy &&
            Width > 0 &&
            Height > 0 &&
            X > 0 &&
            Y > 0;
    }
}
