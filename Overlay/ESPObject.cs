using SkiaSharp;
using JaysAi.Finale.AI;
using JaysAi.Finale.Math;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Overlay
{
    public class EspObject
    {
        private readonly AppSettings _settings;

        public EspObject(AppSettings settings)
        {
            _settings = settings;
        }

        public void Draw(SKCanvas canvas, SKPaint paint, Enemy enemy, Vector2 screenPos)
        {
            if (!enemy.IsAlive || screenPos == default)
                return;

            paint.IsAntialias = true;

            if (_settings.ESP_DrawBox)
            {
                float boxWidth = _settings.ESP_BoxWidth;
                float boxHeight = _settings.ESP_BoxHeight;

                paint.Style = SKPaintStyle.Stroke;
                paint.Color = SKColors.Red;
                paint.StrokeWidth = 1.5f;

                canvas.DrawRect(screenPos.X - boxWidth / 2, screenPos.Y - boxHeight, boxWidth, boxHeight, paint);
            }

            if (_settings.ESP_ShowHealthBar)
            {
                float barHeight = 40;
                float healthPct = enemy.Health / 100f;
                float barWidth = 4;

                paint.Style = SKPaintStyle.Fill;
                paint.Color = SKColors.Gray;
                canvas.DrawRect(screenPos.X - 30, screenPos.Y - barHeight, barWidth, barHeight, paint);

                paint.Color = SKColor.FromHsv(healthPct * 120, 1, 1);
                canvas.DrawRect(screenPos.X - 30, screenPos.Y - (barHeight * healthPct), barWidth, barHeight * healthPct, paint);
            }

            if (_settings.ESP_ShowDistance)
            {
                paint.Color = SKColors.White;
                paint.TextSize = 14;
                canvas.DrawText($"{enemy.Distance:F1}m", screenPos.X + 5, screenPos.Y - 5, paint);
            }
        }
    }
}
