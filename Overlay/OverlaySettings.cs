// neural v3.0
using System.Windows.Media;

namespace JaysAi.Finale.Overlay
{
    public class OverlaySettings
    {
        public bool ShowESP { get; set; } = true;
        public bool ShowCrosshair { get; set; } = true;
        public bool ShowBoundingBoxes { get; set; } = true;
        public bool ShowSnapLines { get; set; } = true;
        public bool EnableChams { get; set; } = false;
        public bool ShowFovCircle { get; set; } = true;
        public double FovRadius { get; set; } = 100.0;

        public Color FriendlyColor { get; set; } = Colors.Green;
        public Color EnemyColor { get; set; } = Colors.Red;
        public Color SnapLineColor { get; set; } = Colors.Yellow;
        public Color FovCircleColor { get; set; } = Colors.White;

        public double OverlayOpacity { get; set; } = 0.85;
        public double LineThickness { get; set; } = 2.0;
        public double BoxThickness { get; set; } = 1.5;
        public bool UseDynamicColors { get; set; } = false;

        public bool ShowHealthBars { get; set; } = false;
        public bool ShowNames { get; set; } = false;
        public bool ShowDistance { get; set; } = false;
    }
}
