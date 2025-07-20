// Monarch v1.0 – OverlayDrawer.cs
// ✅ Monarch Fix Checklist
// [x] Modular WPF ESP drawing logic
// [x] Supports reuse across overlay types
// [x] Takes in ESPObject and draws it cleanly

using JaysAi.Finale.Structs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JaysAi.Finale.Visuals
{
    public static class OverlayDrawer
    {
        public static void DrawESPObject(Canvas canvas, ESPObject obj)
        {
            if (!obj.IsEnemy) return;

            // Bounding box
            var rect = new Rectangle
            {
                Width = obj.Width,
                Height = obj.Height,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            Canvas.SetLeft(rect, obj.X);
            Canvas.SetTop(rect, obj.Y);
            canvas.Children.Add(rect);

            // Label
            var label = new TextBlock
            {
                Text = obj.Label,
                Foreground = Brushes.White,
                FontWeight = FontWeights.SemiBold
            };

            Canvas.SetLeft(label, obj.X);
            Canvas.SetTop(label, obj.Y - 18);
            canvas.Children.Add(label);

            // Optional: Health bar or confidence indicator
            if (obj.Confidence > 0)
            {
                var confBar = new Rectangle
                {
                    Width = obj.Width * obj.Confidence,
                    Height = 4,
                    Fill = Brushes.Green
                };

                Canvas.SetLeft(confBar, obj.X);
                Canvas.SetTop(confBar, obj.Y + obj.Height + 2);
                canvas.Children.Add(confBar);
            }
        }
    }
}
