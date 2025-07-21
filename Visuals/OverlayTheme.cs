//monarch v2.1 – Overlay Theme Manager
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public static class OverlayTheme
    {
        public static SolidColorBrush Enemy => OverlayColor.Red;
        public static SolidColorBrush Teammate => OverlayColor.Blue;
        public static SolidColorBrush Squad => OverlayColor.Green;
        public static SolidColorBrush Background => OverlayColor.Black;
        public static SolidColorBrush HealthBar => OverlayColor.Green;
        public static SolidColorBrush ShieldBar => OverlayColor.Cyan;
        public static SolidColorBrush BoxBorder => OverlayColor.White;
        public static SolidColorBrush AimAssistHighlight => OverlayColor.Yellow;
        public static SolidColorBrush SnapLine => OverlayColor.Orange;
    }
}
