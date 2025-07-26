// Neural v3.0 — DrawConfig.cs
using SkiaSharp;
using System.Windows.Media;

namespace JaysAi.Finale.Overlay
{
    public static class DrawConfig
    {
        // === Crosshair Settings ===
        public static bool CrosshairEnabled { get; set; } = true;
        public static float CrosshairSize { get; set; } = 10f;
        public static float CrosshairThickness { get; set; } = 2f;
        public static SKColor CrosshairColor { get; set; } = new SKColor(255, 0, 0, 200); // Red

        // === Debug Console ===
        public static bool DebugOverlayEnabled { get; set; } = true;
        public static System.Windows.Media.Color DebugTextColor { get; set; } = Colors.Lime;
        public static System.Windows.Media.Color DebugBackgroundColor { get; set; } = Colors.Black;
        public static double DebugOpacity { get; set; } = 0.95;

        // === ESP Styling ===
        public static float EspBoxThickness { get; set; } = 2f;
        public static SKColor EspBoxColor { get; set; } = SKColors.Cyan;
        public static SKColor EspHealthColor { get; set; } = SKColors.Green;
        public static SKColor EspSkeletonColor { get; set; } = SKColors.Yellow;

        // === Outline or Fill Options ===
        public static bool EnableBoxFill { get; set; } = false;
        public static SKColor BoxFillColor { get; set; } = new SKColor(0, 255, 255, 50); // semi-transparent cyan

        // === General Rendering Toggles ===
        public static bool AntiAliasEnabled { get; set; } = true;
        public static bool UseRoundedCorners { get; set; } = true;

        // === Reserved Future Slots ===
        public static bool ExperimentalRenderingEnabled { get; set; } = false;
    }
}
