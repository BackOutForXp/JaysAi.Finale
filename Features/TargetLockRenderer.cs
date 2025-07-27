// Neural v3.1
using JaysAi.Finale.Data;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System;

namespace JaysAi.Finale.Features
{
    public class TargetLockRenderer : IOverlayRenderer
    {
        public bool IsActive => UserSettings.Instance.Get("TargetLockEnabled", true);

        private TrackedTarget _target;

        public void SetTarget(TrackedTarget target)
        {
            _target = target;
        }

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || _target == null || !_target.IsVisible || !_target.ScreenBox.HasValue)
                return;

            var box = _target.ScreenBox.Value;
            float centerX = box.X + box.Width / 2;
            float centerY = box.Y + box.Height / 2;
            float radius = Math.Min(box.Width, box.Height) / 2 + 10;

            var color = UserSettings.Instance.Get("TargetLockColor", SKColors.Red);

            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2f,
                Color = color,
                IsAntialias = true
            };

            canvas.DrawCircle(centerX, centerY, radius, paint);
        }
    }
}
