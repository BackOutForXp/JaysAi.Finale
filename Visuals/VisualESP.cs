//monarch v2.1
using JaysAi.Finale.AI;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Visuals
{
    public class VisualESP
    {
        private readonly ESPSettings settings;

        public VisualESP(ESPSettings settings)
        {
            this.settings = settings;
        }

        public void Draw(SKCanvas canvas, List<FrameSnapshot> entities)
        {
            foreach (var entity in entities)
            {
                if (!settings.ShouldDisplay(entity.Type))
                    continue;

                var color = settings.GetColorForType(entity.Type);

                // Draw bounding box
                if (settings.ShowBoxes)
                {
                    using var paint = new SKPaint
                    {
                        Color = color,
                        StrokeWidth = 2,
                        IsStroke = true,
                        IsAntialias = true
                    };
                    var rect = new SKRect(entity.X - 25, entity.Y - 50, entity.X + 25, entity.Y + 50);
                    canvas.DrawRect(rect, paint);
                }

                // Draw snapline
                if (settings.ShowSnaplines)
                {
                    using var snapPaint = new SKPaint
                    {
                        Color = color.WithAlpha(128),
                        StrokeWidth = 1.5f,
                        IsStroke = true,
                        IsAntialias = true
                    };
                    canvas.DrawLine(settings.ScreenCenterX, settings.ScreenCenterY, entity.X, entity.Y, snapPaint);
                }

                // Draw health bar
                if (settings.ShowHealthBars && entity.Health >= 0)
                {
                    float barHeight = 40;
                    float filled = barHeight * (entity.Health / 100f);
                    using var healthPaint = new SKPaint
                    {
                        Color = SKColors.LimeGreen,
                        IsAntialias = true
                    };
                    var barX = entity.X - 30;
                    var barY = entity.Y - 50;
                    canvas.DrawRect(barX, barY, 4, barHeight, SKPaints.Gray);
                    canvas.DrawRect(barX, barY + (barHeight - filled), 4, filled, healthPaint);
                }
            }
        }
    }
}
