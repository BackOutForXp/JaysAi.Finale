// Neural v3.0 — DetectedObject.cs
using SkiaSharp;

namespace JaysAi.Finale.Modules
{
    public class DetectedObject
    {
        public string Label { get; set; } = "Unknown";
        public float Confidence { get; set; } = 0f;

        // World space or local 3D positions (optional)
        public float WorldX { get; set; }
        public float WorldY { get; set; }
        public float WorldZ { get; set; }

        // 2D screen projection for drawing
        public float ScreenPositionX { get; set; }
        public float ScreenPositionY { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }

        public bool IsVisible { get; set; } = true;

        public SKRect GetScreenRect()
        {
            return new SKRect(
                ScreenPositionX,
                ScreenPositionY,
                ScreenPositionX + Width,
                ScreenPositionY + Height
            );
        }

        public bool IsValid =>
            IsVisible &&
            Width > 1 &&
            Height > 1 &&
            ScreenPositionX >= 0 &&
            ScreenPositionY >= 0;
    }
}
