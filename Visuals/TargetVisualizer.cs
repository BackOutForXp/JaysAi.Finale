//monarch v2.0
using System.Windows;
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public static class TargetVisualizer
    {
        private static readonly Brush EnemyBrush = Brushes.Red;
        private static readonly Brush AllyBrush = Brushes.Green;
        private static readonly Typeface LabelFont = new("Segoe UI");

        public static void Draw(DrawingContext context)
        {
            foreach (var target in ESPModule.ActiveTargets)
            {
                var boxTopLeft = new Point(target.ScreenPosition.X - target.Width / 2, target.ScreenPosition.Y - target.Height / 2);
                var boxSize = new Size(target.Width, target.Height);
                var brush = target.IsEnemy ? EnemyBrush : AllyBrush;
                var pen = new Pen(brush, 1.5);

                // Draw bounding box
                context.DrawRectangle(null, pen, new Rect(boxTopLeft, boxSize));

                // Draw label
                if (!string.IsNullOrWhiteSpace(target.Label))
                {
                    var text = new FormattedText(
                        target.Label,
                        System.Globalization.CultureInfo.InvariantCulture,
                        FlowDirection.LeftToRight,
                        LabelFont,
                        12,
                        brush,
                        1.25);

                    var labelPos = new Point(target.ScreenPosition.X - text.Width / 2, boxTopLeft.Y - 16);
                    context.DrawText(text, labelPos);
                }
            }
        }
    }
}
