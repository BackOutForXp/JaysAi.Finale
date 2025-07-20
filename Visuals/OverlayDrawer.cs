//monarch v2.1 – Overlay Visual Renderer
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Visuals
{
    public class OverlayDrawer
    {
        private readonly Canvas _canvas;
        private readonly OverlaySignal _signal;

        public OverlayDrawer(Canvas canvas, OverlaySignal signal)
        {
            _canvas = canvas;
            _signal = signal;
        }

        public void DrawAll()
        {
            _canvas.Children.Clear();

            var commands = _signal.FetchAndClear();
            foreach (var command in commands)
            {
                switch (command.Type.ToLower())
                {
                    case "box":
                        DrawBox(command.X, command.Y, command.W, command.H);
                        break;
                    case "line":
                        DrawLine(command.X, command.Y, command.W, command.H);
                        break;
                    default:
                        break;
                }
            }
        }

        private void DrawBox(float x, float y, float width, float height)
        {
            var box = new Rectangle
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            Canvas.SetLeft(box, x);
            Canvas.SetTop(box, y);
            _canvas.Children.Add(box);
        }

        private void DrawLine(float x1, float y1, float x2, float y2)
        {
            var line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.LimeGreen,
                StrokeThickness = 1
            };

            _canvas.Children.Add(line);
        }
    }
}
