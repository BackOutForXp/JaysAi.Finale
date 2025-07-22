//heavenly v3.0.0 – ESP Style Configurator
using System.Windows.Media;

namespace JaysAi.Finale.AI
{
    public static class ESPStyleConfig
    {
        public static SolidColorBrush EnemyBoxColor { get; set; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        public static SolidColorBrush TeamBoxColor { get; set; } = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        public static SolidColorBrush SquadBoxColor { get; set; } = new SolidColorBrush(Color.FromRgb(0, 128, 255));
        public static SolidColorBrush TrackedBoxColor { get; set; } = new SolidColorBrush(Color.FromRgb(255, 255, 0));
        public static SolidColorBrush PredictionPathColor { get; set; } = new SolidColorBrush(Color.FromRgb(255, 165, 0));

        public static double BoxThickness { get; set; } = 2.0;
        public static bool ShowHealthBar { get; set; } = true;
        public static bool ShowDistance { get; set; } = true;
        public static bool ShowLabels { get; set; } = true;
        public static bool EnableFadeOut { get; set; } = true;

        public static void ResetDefaults()
        {
            EnemyBoxColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            TeamBoxColor = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            SquadBoxColor = new SolidColorBrush(Color.FromRgb(0, 128, 255));
            TrackedBoxColor = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            PredictionPathColor = new SolidColorBrush(Color.FromRgb(255, 165, 0));

            BoxThickness = 2.0;
            ShowHealthBar = true;
            ShowDistance = true;
            ShowLabels = true;
            EnableFadeOut = true;
        }
    }
}
