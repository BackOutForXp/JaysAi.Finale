using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JaysAi.Finale.Visuals
{
    public static class RectangleHelper
    {
        public static Rectangle CreateTargetBox(double x, double y, double width, double height, Brush color, double thickness = 2.0)
        {
            return new Rectangle
            {
                Width = width,
                Height = height,
                Stroke = color,
                StrokeThickness = thickness,
                Fill = Brushes.Transparent,
                RadiusX = 0,
                RadiusY = 0,
                Margin = new Thickness(x, y, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
        }

        public static Rectangle CreateLabelBox(double x, double y, string label, Brush color, double fontSize = 12.0)
        {
            // Placeholder for label rendering – WPF typically uses TextBlock, but returning a dummy shape for modular use
            return new Rectangle
            {
                Width = label.Length * fontSize * 0.6,
                Height = fontSize * 1.5,
                Stroke = color,
                StrokeThickness = 1,
                Fill = Brushes.Transparent,
                Margin = new Thickness(x, y, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
        }
    }
}
