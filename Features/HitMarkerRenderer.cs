// Neural v3.1
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System;

namespace JaysAi.Finale.Features
{
    public class HitMarkerRenderer : IOverlayRenderer
    {
        public bool IsActive => UserSettings.Instance.Get("HitMarkerEnabled", true);

        private DateTime _lastHitTime;
        private readonly TimeSpan _duration = TimeSpan.FromMilliseconds(200);

        public void MarkHit()
        {
            _lastHitTime = DateTime.UtcNow;
        }

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || DateTime.UtcNow - _lastHitTime > _duration)
                return;

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;
            float size = UserSettings.Instance.Get("HitMarkerSize", 10f);
            var color = UserSettings.Instance.Get("HitMarkerColor", SKColors.White);

            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2f,
                Color = color,
                IsAntialias = true
            };

            canvas.DrawLine(centerX - size, centerY - size, centerX + size, centerY + size, paint);
            canvas.DrawLine(centerX - size, centerY + size, centerX + size, centerY - size, paint);
        }
    }
}
