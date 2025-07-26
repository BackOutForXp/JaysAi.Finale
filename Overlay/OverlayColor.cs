// Neural v3.0 — OverlayColor.cs
using SkiaSharp;
using System.Windows.Media;

namespace JaysAi.Finale.Overlay
{
    public static class OverlayColor
    {
        // === Crosshair ===
        public static SKColor Crosshair = new SKColor(255, 0, 0, 200); // Red
        public static SKColor CrosshairFill = new SKColor(255, 0, 0, 60); // Transparent red

        // === FOV ===
        public static SKColor FovRing = new SKColor(255, 255, 0, 180); // Yellow
        public static SKColor FovFill = new SKColor(255, 255, 0, 50);   // Transparent yellow

        // === ESP ===
        public static SKColor EspBox = new SKColor(0, 255, 255, 220); // Cyan
        public static SKColor EspHealth = new SKColor(0, 255, 0, 220); // Green
        public static SKColor EspSkeleton = new SKColor(255, 255, 255, 220); // White

        // === Debug Console ===
        public static Color DebugText = Colors.Lime;
        public static Color DebugBackground = Colors.Black;

        // === Outlines & Highlights ===
        public static SKColor Outline = new SKColor(255, 255, 255, 60);
        public static SKColor Highlight = new SKColor(255, 0, 255, 180); // Magenta

        // === Neutral / Error ===
        public static SKColor Warning = new SKColor(255, 165, 0); // Orange
        public static SKColor Error = new SKColor(255, 0, 0);     // Red
        public static SKColor Transparent = new SKColor(0, 0, 0, 0); // Fully transparent

        // === Reserved Future Colors ===
        public static SKColor Reserved1 = new SKColor(200, 200, 200, 80);
        public static SKColor Reserved2 = new SKColor(100, 255, 180, 140);
    }
}
