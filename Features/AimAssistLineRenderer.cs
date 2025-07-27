// Neural v3.1
using JaysAi.Finale.Data;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using SkiaSharp;

namespace JaysAi.Finale.Features
{
    public class AimAssistLineRenderer : IOverlayRenderer
    {
        public bool IsActive => UserSettings.Instance.Get("AimLineEnabled", true);

        private TrackedTarget _target;

        public void SetTarget(TrackedTarget target)
        {
            _target = target;
        }

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || _target == null || !_target.IsVisible || !_target.ScreenBox.HasValue)
                return;

            var targetBox = _target.ScreenBox.Value;
            float targetX = targetBox.X + targetBox.Width / 2f;
            float targetY = targetBox.Y + targetBox.Height / 2f;

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;

            var color = UserSettings.Instance.Get("AimLineColor", SKColors.Blue);

            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1.5f,
                Color = color,
                IsAntialias = true
            };

            canvas.DrawLine(centerX, centerY, targetX, targetY, paint);
        }
    }
}
