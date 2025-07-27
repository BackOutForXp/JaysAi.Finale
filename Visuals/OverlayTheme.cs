// Neural v3.1 — OverlayTheme.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public static class OverlayTheme
    {
        public static SKColor EspBoxColor => new(0, 255, 0, 255); // Green
        public static SKColor EspBoxFillColor => new(0, 255, 0, 50); // Transparent green fill
        public static SKColor CrosshairColor => new(255, 0, 0, 255); // Red
        public static SKColor FovCircleColor => new(0, 128, 255, 200); // Blue
        public static SKColor LabelTextColor => new(255, 255, 255, 255); // White
        public static SKColor BackgroundShadow => new(0, 0, 0, 150); // Transparent black

        public static float LineThickness => 2.5f;
        public static float LabelFontSize => 14f;
    }
}
