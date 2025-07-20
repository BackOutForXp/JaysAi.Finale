//monarch v2.0
using System.Windows.Media;
using System.Windows;

namespace JaysAi.Finale.Visuals
{
    public static class CrosshairRenderer
    {
        public static void Draw(DrawingContext context, double screenWidth, double screenHeight)
        {
            var center = new Point(screenWidth / 2, screenHeight / 2);
            double length = 8;
            double thickness = 1.5;
            Brush brush = GetBrush();

            // Horizontal Line
            context.DrawLine(new Pen(brush, thickness),
                new Point(center.X - length, center.Y),
                new Point(center.X + length, center.Y));

            // Vertical Line
            context.DrawLine(new Pen(brush, thickness),
                new Point(center.X, center.Y - length),
                new Point(center.X, center.Y + length));
        }

        private static Brush GetBrush()
        {
            return OverlaySignal.CurrentSignal switch
            {
                OverlaySignal.SignalType.TargetAcquired => Brushes.LimeGreen,
                OverlaySignal.SignalType.AlertFlash => Brushes.Red,
                OverlaySignal.SignalType.TargetLost => Brushes.Gray,
                _ => Brushes.White
            };
        }
    }
}
