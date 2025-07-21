//monarch v2.1 – AI Overlay Handler
using System.Windows;
using System.Windows.Media;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Visuals
{
    public static class AiOverlay
    {
        public static void QueueRectangle(double x, double y, double width, double height, string label, SolidColorBrush color)
        {
            var rectangle = new OverlayRectangle
            {
                X = x,
                Y = y,
                Width = width,
                Height = height,
                Label = label,
                Color = color
            };

            OverlaySignal.Enqueue(rectangle);
        }
    }

    public class OverlayRectangle
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Label { get; set; }
        public SolidColorBrush Color { get; set; }
    }
}
