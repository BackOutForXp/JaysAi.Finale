using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JaysAi.Finale.Visuals
{
    public class FovOverlayRenderer : OverlayBase
    {
        private Ellipse _fovCircle = new();
        private double _fovAngle = 90;

        public FovOverlayRenderer()
        {
            Loaded += (s, e) => DrawFovCircle();
        }

        public void UpdateFov(double newFov)
        {
            _fovAngle = newFov;
            DrawFovCircle();
        }

        private void DrawFovCircle()
        {
            double radius = _fovAngle / 180.0 * (Width / 2.0);

            _fovCircle.Width = radius * 2;
            _fovCircle.Height = radius * 2;
            _fovCircle.Stroke = Brushes.Red;
            _fovCircle.StrokeThickness = 2;
            _fovCircle.Fill = Brushes.Transparent;

            Canvas canvas = new();
            canvas.Width = Width;
            canvas.Height = Height;
            canvas.Children.Clear();
            canvas.Children.Add(_fovCircle);

            Canvas.SetLeft(_fovCircle, (Width - _fovCircle.Width) / 2);
            Canvas.SetTop(_fovCircle, (Height - _fovCircle.Height) / 2);

            Content = canvas;
        }
    }
}
