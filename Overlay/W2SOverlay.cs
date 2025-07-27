using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.Math;
using JaysAi.Finale.Memory;
using JaysAi.Finale.Rendering;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public class W2SOverlay
    {
        private readonly WorldToScreenConverter _w2s;
        private readonly AppSettings _settings;

        public W2SOverlay(WorldToScreenConverter w2s, AppSettings settings)
        {
            _w2s = w2s;
            _settings = settings;
        }

        public void Draw(SKCanvas canvas, SKPaint paint, List<Enemy> enemies)
        {
            if (!_settings.EnableESP || enemies == null || enemies.Count == 0)
                return;

            foreach (var enemy in enemies)
            {
                if (!enemy.IsAlive || enemy.Position == default)
                    continue;

                if (_w2s.TryProject(enemy.Position, out var screenPos))
                {
                    paint.Color = SKColors.Red;
                    paint.IsAntialias = true;

                    canvas.DrawCircle(screenPos.X, screenPos.Y, 4, paint);

                    if (_settings.ESP_ShowDistance)
                    {
                        string distanceText = $"{enemy.Distance:F1}m";
                        paint.TextSize = 16;
                        canvas.DrawText(distanceText, screenPos.X + 6, screenPos.Y - 6, paint);
                    }
                }
            }
        }
    }
}
