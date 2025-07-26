// Neural v3.0 — OverlayTheme.cs
using SkiaSharp;
using System.Windows.Media;

namespace JaysAi.Finale.Overlay
{
    public static class OverlayTheme
    {
        // === Global Theme Colors ===
        public static SKColor PrimaryColor { get; set; } = new SKColor(0, 255, 255); // Cyan
        public static SKColor AccentColor { get; set; } = new SKColor(255, 0, 255);  // Magenta
        public static SKColor BackgroundColor { get; set; } = new SKColor(20, 20, 20, 220); // Dark semi-transparent
        public static SKColor HighlightColor { get; set; } = new SKColor(255, 255, 0); // Yellow
        public static SKColor DisabledColor { get; set; } = new SKColor(100, 100, 100, 150); // Dim gray

        // === Text Styling ===
        public static SKColor TextColor { get; set; } = new SKColor(0, 255, 0); // Lime
        public static float TextSize { get; set; } = 14f;
        public static string FontFamily { get; set; } = "Consolas";

        // === Opacity and Stroke Defaults ===
        public static float DefaultOpacity { get; set; } = 0.9f;
        public static float StrokeThickness { get; set; } = 2f;

        // === Margin and Padding ===
        public static float Padding { get; set; } = 6f;
        public static float CornerRadius { get; set; } = 4f;

        // === Visual Feedback ===
        public static SKColor SuccessColor { get; set; } = new SKColor(0, 255, 0);
        public static SKColor WarningColor { get; set; } = new SKColor(255, 165, 0);
        public static SKColor ErrorColor { get; set; } = new SKColor(255, 0, 0);

        // === Debug Console Theme ===
        public static System.Windows.Media.Color DebugBackground { get; set; } = Colors.Black;
        public static System.Windows.Media.Color DebugTextColor { get; set; } = Colors.Lime;

        // === Reserved Future Themes ===
        public static SKColor Reserved1 { get; set; } = new SKColor(70, 70, 70, 100);
    }
}
