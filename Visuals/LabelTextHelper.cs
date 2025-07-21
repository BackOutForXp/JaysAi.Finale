using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public static class LabelTextHelper
    {
        public static TextBlock CreateOverlayLabel(string text, double x, double y, Brush color, double fontSize = 12.0)
        {
            return new TextBlock
            {
                Text = text,
                Foreground = color,
                FontSize = fontSize,
                FontWeight = FontWeights.Bold,
                Margin = new System.Windows.Thickness(x, y, 0, 0),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top
            };
        }
    }
}
