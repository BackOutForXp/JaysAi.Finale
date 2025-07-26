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
        public System.Windows.Media.Color LineColor { get; set; } = Colors.Red;
        public bool DynamicCentering { get; set; } = true;
        public System.Windows.Point? TargetPoint { get; set; }

        public void Draw(DrawingContext context, double centerX, double centerY)
        {
            if (!Enabled || context == null) return;

            System.Windows.Point position = TargetPoint ?? new Point(centerX, centerY);
            System.Windows.Media.Pen pen = new(new SolidColorBrush(LineColor), Thickness);

            // Horizontal line
            context.DrawLine(pen,
                new System.Windows.Point(position.X - Length, position.Y),
                new System.Windows.Point(position.X + Length, position.Y));

            // Vertical line
            context.DrawLine(pen,
                new System.Windows.Point(position.X, position.Y - Length),
                new System.Windows.Point(position.X, position.Y + Length));
        }
    }
}
