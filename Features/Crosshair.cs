// heavenly v3.0 – Dynamic Crosshair Overlay Logic
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace JaysAi.Finale.Features
{
    public class Crosshair
    {
        private readonly double size;
        private readonly Brush color;
        private readonly double thickness;

        public Crosshair(double size = 12.0, Brush? color = null, double thickness = 2.0)
        {
            this.size = size;
            this.color = color ?? Brushes.Red;
            this.thickness = thickness;
        }

        public void Draw(DrawingContext dc, Point center)
        {
            Pen pen = new Pen(color, thickness);

            // Horizontal line
            dc.DrawLine(pen, new Point(center.X - size, center.Y), new Point(center.X + size, center.Y));
            // Vertical line
            dc.DrawLine(pen, new Point(center.X, center.Y - size), new Point(center.X, center.Y + size));
        }
    }
}
