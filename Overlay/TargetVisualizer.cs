// Neural v3.0 — TargetVisualizer.cs
using SkiaSharp;
using System.Collections.Generic;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.Overlay
{
    public class TargetVisualizer
    {
        public bool IsEnabled { get; set; } = true;
        public float BoxThickness { get; set; } = 2f;
        public SKColor LockColor { get; set; } = SKColors.Green;
        public SKColor TrailColor { get; set; } = new SKColor(0, 255, 255, 100);
        public bool ShowTrail { get; set; } = false;

        private List<SKPoint> _trailPoints = new();

        /// <summary>
        /// Draws a visual indicator on a target.
        /// </summary>
        public void Draw(SKCanvas canvas, TargetData target)
        {
            if (!IsEnabled || target == null || canvas == null || !target.IsVisible)
                return;

            var box = target.ScreenBox;

            if (box != null)
            {
                // Draw lock box
                using var paint = new SKPaint
                {
                    Color = LockColor,
                    StrokeWidth = BoxThickness,
                    Style = SKPaintStyle.Stroke,
                    IsAntialias = true
                };

                canvas.DrawRect(box.Value.X, box.Value.Y, box.Value.Width, box.Value.Height, paint);
            }

            // Draw trail if enabled
            if (ShowTrail && target.CenterScreenPoint != null)
            {
                _trailPoints.Add(target.CenterScreenPoint.Value);
                TrimTrail();

                using var trailPaint = new SKPaint
                {
                    Color = TrailColor,
                    StrokeWidth = 1.5f,
                    Style = SKPaintStyle.Stroke,
                    IsAntialias = true
                };

                for (int i = 1; i < _trailPoints.Count; i++)
                {
                    canvas.DrawLine(_trailPoints[i - 1], _trailPoints[i], trailPaint);
                }
            }
        }

        private void TrimTrail()
        {
            const int MaxTrailLength = 50;
            if (_trailPoints.Count > MaxTrailLength)
                _trailPoints.RemoveRange(0, _trailPoints.Count - MaxTrailLength);
        }

        public void ClearTrail() => _trailPoints.Clear();

        public void SetTrailColor(SKColor color) => TrailColor = color;

        public void SetLockColor(SKColor color) => LockColor = color;

        public void EnableTrail(bool enabled) => ShowTrail = enabled;
    }
}
