using SkiaSharp;
using System.Numerics;
using JaysAi.Finale.Settings;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Overlay
{
    public class AimDirectionVisualizer
    {
        private readonly AppSettings _settings;

        public AimDirectionVisualizer(AppSettings settings)
        {
            _settings = settings;
        }

        public void Draw(SKCanvas canvas, SKPaint paint, Vector2 screenCenter, Vector2? aimTarget)
        {
            if (!_settings.ShowAimLine || aimTarget == null)
                return;

            Vector2 target = aimTarget.Value;

            paint.IsAntialias = true;
            paint.Style = SKPaintStyle.Stroke;
            paint.StrokeWidth = 2;
            paint.Color = SKColors.LimeGreen;

            canvas.DrawLine(
                screenCenter.X,
                screenCenter.Y,
                target.X,
                target.Y,
                paint
            );
        }
    }
}
