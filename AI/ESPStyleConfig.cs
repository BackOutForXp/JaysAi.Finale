// neural v3.0
using System.Windows.Media;

namespace JaysAi.Finale.AI
{
    public static class ESPStyleConfig
    {
        // Colors
        public static SolidColorBrush EnemyColor { get; set; } = new(System.Windows.Media.Color.FromRgb(255, 0, 0));     // Red
        public static SolidColorBrush TeamColor { get; set; } = new(System.Windows.Media.Color.FromRgb(0, 255, 0));      // Green
        public static SolidColorBrush NeutralColor { get; set; } = new(System.Windows.Media.Color.FromRgb(255, 255, 0)); // Yellow
        public static SolidColorBrush HealthBarColor { get; set; } = new(System.Windows.Media.Color.FromRgb(0, 200, 0)); // Darker Green
        public static SolidColorBrush OutlineColor { get; set; } = new(System.Windows.Media.Color.FromRgb(0, 0, 0));     // Black

        // Styling
        public static double LineThickness { get; set; } = 2.0;
        public static double FontSize { get; set; } = 14;
        public static double HealthBarWidth { get; set; } = 4.0;
        public static double SnapLineOpacity { get; set; } = 0.7;

        // Outline Control
        public static bool EnableOutline { get; set; } = true;

        // Font family (optional future expansion)
        public static string FontFamily { get; set; } = "Segoe UI";

        public static void ApplyPreset(string theme)
        {
            switch (theme.ToLower())
            {
                case "dark":
                    EnemyColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 80, 80));
                    TeamColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(80, 255, 80));
                    NeutralColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 150));
                    break;

                case "neon":
                    EnemyColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 255)); // Pink
                    TeamColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 255)); // Cyan
                    NeutralColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 0));
                    break;
            }
        }
    }
}
