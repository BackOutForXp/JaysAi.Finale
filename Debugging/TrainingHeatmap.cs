using System.Collections.Generic;
using SkiaSharp;

namespace JaysAi.Finale.Debugging
{
    public static class TrainingHeatmap
    {
        private static readonly List<SKPoint> _impactPoints = new();
        private static readonly int _maxPoints = 500;
        private static readonly object _lock = new();

        public static bool Enabled { get; set; } = true;
        public static float PointRadius = 10f;

        public static void Record(SKPoint screenPosition)
        {
            if (!Enabled) return;

            lock (_lock)
            {
                _impactPoints.Add(screenPosition);

                if (_impactPoints.Count > _maxPoints)
                    _impactPoints.RemoveAt(0);
            }
        }

        public static void Draw(SKCanvas canvas)
        {
            if (!Enabled) return;

            lock (_lock)
            {
                foreach (var point in _impactPoints)
                {
                    using var paint = new SKPaint
                    {
                        Color = new SKColor(255, 69, 0, 80), // orange-red
                        IsAntialias = true
                    };

                    canvas.DrawCircle(point, PointRadius, paint);
                }
            }
        }

        public static void Clear()
        {
            lock (_lock)
            {
                _impactPoints.Clear();
            }
        }
    }
}
