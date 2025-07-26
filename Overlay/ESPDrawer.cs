// Neural v3.0 — EspDrawer.cs
using JaysAi.Finale.Data;
using JaysAi.Finale.Features;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Modules;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public class EspDrawer
    {
        private readonly Modules.ESPModule _espModule;

        public EspDrawer(Modules.ESPModule espModule)
        {
            _espModule = espModule;
        }

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!_espModule.IsEnabled || _espModule.Targets == null)
                return;

            using var paint = new SKPaint
            {
                IsAntialias = DrawConfig.AntiAliasEnabled,
                StrokeWidth = DrawConfig.EspBoxThickness,
                Style = SKPaintStyle.Stroke,
                Color = DrawConfig.EspBoxColor
            };

            foreach (var target in _espModule.Targets)
            {
                if (!target.IsVisible || target.ScreenBox == null)
                    continue;

                var box = target.ScreenBox.Value;
                var left = box.X;
                var top = box.Y;
                var right = box.X + box.Width;
                var bottom = box.Y + box.Height;

                // Draw filled box background if enabled
                if (DrawConfig.EnableBoxFill)
                {
                    using var fillPaint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = DrawConfig.BoxFillColor
                    };
                    canvas.DrawRect(left, top, box.Width, box.Height, fillPaint);
                }

                // Draw main ESP box
                canvas.DrawRect(left, top, box.Width, box.Height, paint);

                // Draw health bar
                if (target.Health > 0 && target.MaxHealth > 0)
                {
                    float healthRatio = target.Health / target.MaxHealth;
                    float barHeight = box.Height * healthRatio;
                    float barTop = bottom - barHeight;

                    using var healthPaint = new SKPaint
                    {
                        Color = DrawConfig.EspHealthColor,
                        Style = SKPaintStyle.Fill
                    };

                    canvas.DrawRect(left - 5, barTop, 3, barHeight, healthPaint);
                }

                // Draw name
                if (!string.IsNullOrWhiteSpace(target.Name))
                {
                    using var textPaint = new SKPaint
                    {
                        Color = DrawConfig.EspBoxColor,
                        TextSize = 14,
                        IsAntialias = true,
                        Typeface = SKTypeface.Default
                    };

                    canvas.DrawText(target.Name, left, top - 4, textPaint);
                }

                // Optional: draw skeleton or ID
                if (target.SkeletonPoints != null && DrawConfig.ExperimentalRenderingEnabled)
                {
                    DrawSkeleton(canvas, target.SkeletonPoints);
                }
            }
        }

        private void DrawSkeleton(SKCanvas canvas, List<SKPoint> points)
        {
            if (points.Count < 2) return;

            using var skeletonPaint = new SKPaint
            {
                Color = DrawConfig.EspSkeletonColor,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1.5f,
                IsAntialias = true
            };

            for (int i = 1; i < points.Count; i++)
            {
                canvas.DrawLine(points[i - 1], points[i], skeletonPaint);
            }
        }
    }
}
