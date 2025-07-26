// neural v3.0
using System.Windows.Media;
using System.Windows;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Features
{
    public class Crosshair
    {
        public bool Enabled { get; set; } = true;
        public double Thickness { get; set; } = 1.5;
        public double Length { get; set; } = 10;
        public Color LineColor { get; set; } = Colors.Red;
        public bool DynamicCentering { get; set; } = true;
        public Point? TargetPoint { get; set; }

        public void Draw(DrawingContext context, double centerX, double centerY)
        {
            if (!Enabled || context == null) return;

            Point position = TargetPoint ?? new Point(centerX, centerY);
            Pen pen = new(new SolidColorBrush(LineColor), Thickness);

            // Horizontal line
            context.DrawLine(pen,
                new Point(position.X - Length, position.Y),
                new Point(position.X + Length, position.Y));

            // Vertical line
            context.DrawLine(pen,
                new Point(position.X, position.Y - Length),
                new Point(position.X, position.Y + Length));
        }
    }
}
