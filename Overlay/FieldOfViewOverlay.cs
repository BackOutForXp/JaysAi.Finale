using SkiaSharp;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Overlay
{
    public class FieldOfViewOverlay
    {
        private readonly AppSettings _settings;

        public FieldOfViewOverlay(AppSettings settings)
        {
            _settings = settings;
        }

        public void Draw(SKCanvas canvas, SKPaint paint, SKPoint screenCenter, float radius)
        {
            if (!_settings.ShowFOVCircle)
                return;

            paint.IsAntialias = true;
            paint.Style = SKPaintStyle.Stroke;
            paint.StrokeWidth = 2;
            paint.Color = SKColors.Orange;

            canvas.DrawCircle(screenCenter.X, screenCenter.Y, radius, paint);
        }
    }
}
