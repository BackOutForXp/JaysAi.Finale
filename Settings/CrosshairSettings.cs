// Neural v3.1 — CrosshairSettings.cs
using SkiaSharp;

namespace JaysAi.Finale.Settings
{
    public class CrosshairSettings
    {
        public bool Enabled { get; set; } = true;
        public float Thickness { get; set; } = 2f;
        public float Length { get; set; } = 15f;
        public float Gap { get; set; } = 5f;
        public bool CenterDot { get; set; } = false;
        public float DotSize { get; set; } = 3f;
        public SKColor Color { get; set; } = SKColors.Red;
        public bool UseOutline { get; set; } = true;
        public float OutlineThickness { get; set; } = 1.5f;
        public SKColor OutlineColor { get; set; } = SKColors.Black;
        public bool AntiAlias { get; set; } = true;
    }
}
