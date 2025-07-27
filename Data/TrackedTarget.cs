// Neural v3.1 — TrackedTarget.cs
using System.Numerics;
using System.Drawing;

namespace JaysAi.Finale.Data
{
    public class TrackedTarget
    {
        public int Id { get; set; }
        public Vector3 Position3D { get; set; }
        public Vector2 ScreenPosition { get; set; }
        public RectangleF? ScreenBox { get; set; }

        public bool IsVisible { get; set; }
        public float Health { get; set; }
        public float Distance { get; set; }
        public Vector3 Velocity { get; set; }

        public bool IsValid =>
            IsVisible &&
            ScreenBox.HasValue &&
            Health > 0 &&
            Distance > 0;
    }
}
