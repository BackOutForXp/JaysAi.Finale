// File: Visuals/CrosshairOverlay.cs
using JaysAi.Finale.Core;
using JaysAi.Finale.Settings;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JaysAi.Finale.Visuals
{
    public class CrosshairOverlay
    {
        private readonly AppSettings _settings;

        public bool IsEnabled { get; set; } = true;

        public CrosshairOverlay(AppSettings settings)
        {
            _settings = settings;
        }

        public void Render(Canvas canvas)
        {
            if (!IsEnabled) return;

            double centerX = canvas.ActualWidth / 2;
            double centerY = canvas.ActualHeight / 2;

            var color = (Color)ColorConverter.ConvertFromString(_settings.CrosshairColorHex);
            var brush = new SolidColorBrush(color);
            double thickness = _settings.CrosshairThickness;
            double length = _settings.CrosshairLength;

            canvas.Children.Clear();

            // Horizontal line
            canvas.Children.Add(new Line
            {
                X1 = centerX - length,
                Y1 = centerY,
                X2 = centerX + length,
                Y2 = centerY,
                Stroke = brush,
                StrokeThickness = thickness
            });

            // Vertical line
            canvas.Children.Add(new Line
            {
                X1 = centerX,
                Y1 = centerY - length,
                X2 = centerX,
                Y2 = centerY + length,
                Stroke = brush,
                StrokeThickness = thickness
            });

            // Center dot (optional)
            if (_settings.ShowCenterDot)
            {
                var dot = new Ellipse
                {
                    Width = thickness * 2,
                    Height = thickness * 2,
                    Fill = brush
                };
                Canvas.SetLeft(dot, centerX - dot.Width / 2);
                Canvas.SetTop(dot, centerY - dot.Height / 2);
                canvas.Children.Add(dot);
            }
        }
    }
}
