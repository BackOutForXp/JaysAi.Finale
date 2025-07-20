//monarch v2.1
using System.Windows.Media;

namespace JaysAi.Finale.AI
{
    public class ESPStyleConfig
    {
        public Color Color { get; set; } = Colors.Red;
        public float Thickness { get; set; } = 2.0f;
        public double Opacity { get; set; } = 1.0;

        public ESPStyleConfig() { }

        public ESPStyleConfig(Color color, float thickness, double opacity)
        {
            Color = color;
            Thickness = thickness;
            Opacity = opacity;
        }
    }
}
