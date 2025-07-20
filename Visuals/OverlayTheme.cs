//monarch v2.1 – Overlay Theme Presets and Color System
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public static class OverlayTheme
    {
        // Enemy-related colors
        public static SKColor EnemyBoxColor { get; set; } = SKColors.Red;
        public static SKColor EnemySnaplineColor { get; set; } = SKColors.DarkRed;
        public static SKColor EnemyFOVCircleColor { get; set; } = SKColors.OrangeRed;

        // Friendly/Team-related colors
        public static SKColor TeamBoxColor { get; set; } = SKColors.Cyan;
        public static SKColor TeamSnaplineColor { get; set; } = SKColors.LightBlue;

        // Crosshair & UI elements
        public static SKColor CrosshairColor { get; set; } = SKColors.LimeGreen;
        public static SKColor BackgroundBoxFill { get; set; } = SKColors.Black.WithAlpha(150);
        public static SKColor TextColor { get; set; } = SKColors.White;

        // Status & debugging
        public static SKColor TargetLockIndicator { get; set; } = SKColors.Gold;
        public static SKColor RecoilRingColor { get; set; } = SKColors.Yellow;

        // Future animated gradient or color override support
        public static bool UseDynamicTheme { get; set; } = false;

        public static void ApplyRedMode()
        {
            EnemyBoxColor = SKColors.Red;
            EnemySnaplineColor = SKColors.DarkRed;
            CrosshairColor = SKColors.Red;
        }

        public static void ApplyStealthMode()
        {
            EnemyBoxColor = SKColors.Gray;
            EnemySnaplineColor = SKColors.DarkGray;
            CrosshairColor = SKColors.WhiteSmoke;
        }

        public static void ApplyNeonMode()
        {
            EnemyBoxColor = SKColors.HotPink;
            CrosshairColor = SKColors.MediumPurple;
            TeamBoxColor = SKColors.Aqua;
        }
    }
}
