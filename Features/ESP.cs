// File: Features/ESP.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Visuals;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Features
{
    public class ESP
    {
        private readonly SettingsManager<AppSettings> _settings;
        private readonly IOverlayRenderer _overlay;
        private readonly IEnemyProvider _enemyProvider;

        public ESP(SettingsManager<AppSettings> settings, IOverlayRenderer overlay, IEnemyProvider enemyProvider)
        {
            _settings = settings;
            _overlay = overlay;
            _enemyProvider = enemyProvider;
        }

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!_settings.Current.ESP.Enabled)
                return;

            List<Enemy> enemies = _enemyProvider.GetVisibleEnemies();
            if (enemies == null || enemies.Count == 0)
                return;

            var color = _settings.Current.ESP.EnemyColor;
            var paint = new SKPaint
            {
                Color = new SKColor(color.R, color.G, color.B, color.A),
                StrokeWidth = 2,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            var textPaint = new SKPaint
            {
                Color = new SKColor(color.R, color.G, color.B),
                TextSize = 16,
                IsAntialias = true
            };

            foreach (var enemy in enemies)
            {
                if (!enemy.IsAlive || !enemy.ScreenBounds.HasValue)
                    continue;

                var bounds = enemy.ScreenBounds.Value;

                if (_settings.Current.ESP.ShowBoxes)
                {
                    canvas.DrawRect(bounds, paint);
                }

                if (_settings.Current.ESP.ShowNames && !string.IsNullOrEmpty(enemy.Name))
                {
                    var namePos = new SKPoint(bounds.MidX, bounds.Top - 5);
                    canvas.DrawText(enemy.Name, namePos, textPaint);
                }
            }
        }
    }
}
