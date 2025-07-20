// File: Visuals/ESPDrawer.cs
using SkiaSharp;
using System.Collections.Generic;
using System.Numerics;
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Visuals
{
    public static class ESPDrawer
    {
        public static void Draw(SKCanvas canvas, List<Enemy> enemies, AppSettings settings)
        {
            if (!settings.EnableESP || canvas == null || enemies == null) return;

            var paint = new SKPaint
            {
                Color = SKColors.LimeGreen,
                StrokeWidth = 2,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            foreach (var enemy in enemies)
            {
                if (!enemy.IsVisible || !enemy.ScreenPosition.HasValue)
                    continue;

                var screenPos = enemy.ScreenPosition.Value;
                float boxWidth = 60;
                float boxHeight = 120;

                SKRect rect = new(
                    screenPos.X - boxWidth / 2,
                    screenPos.Y - boxHeight,
                    screenPos.X + boxWidth / 2,
                    screenPos.Y
                );

                // Draw bounding box
                canvas.DrawRect(rect, paint);

                // Draw head dot if bone info exists
                if (enemy.Bones?.HasValidBones == true)
                {
                    var head = enemy.Bones.Head;
                    canvas.DrawCircle(head.X, head.Y, 5, paint);
                }

                // Draw health bar (optional)
                if (enemy.Health > 0)
                {
                    var healthPaint = new SKPaint
                    {
                        Color = SKColors.Red,
                        Style = SKPaintStyle.Fill,
                        IsAntialias = true
                    };

                    float maxHeight = 120f;
                    float healthHeight = enemy.Health / 100f * maxHeight;

                    var barX = screenPos.X - boxWidth / 2 - 6;
                    var barY = screenPos.Y - healthHeight;

                    canvas.DrawRect(new SKRect(barX, barY, barX + 4, screenPos.Y), healthPaint);
                }
            }
        }
    }
}
