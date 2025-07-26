// Neural v3.0 — VisualEsp.cs
using System.Collections.Generic;
using JaysAi.Finale.Data;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Overlay;
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public class VisualEsp : IOverlayRenderer
    {
        public bool IsActive { get; set; } = true;
        public List<TargetData> Targets { get; set; } = new();

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || canvas == null || Targets == null)
                return;

            foreach (var target in Targets)
            {
                if (!target.IsVisible || target.ScreenBox == null)
                    continue;

                var box = target.ScreenBox.Value;

                // Draw Box
                OverlayShape.DrawBox(
                    canvas,
                    box.X,
                    box.Y,
                    box.Width,
                    box.Height,
                    OverlayColor.EspBox,
                    DrawConfig.EspBoxThickness
                );

                // Draw Health Bar
                if (target.Health > 0 && target.MaxHealth > 0 && DrawConfig.EnableBoxFill)
                {
                    float ratio = target.Health / target.MaxHealth;
                    float barHeight = box.Height * ratio;
                    float barTop = box.Bottom - barHeight;

                    canvas.DrawRect(
                        box.X - 5,
                        barTop,
                        3,
                        barHeight,
                        new SKPaint
                        {
                            Color = OverlayColor.EspHealth,
                            Style = SKPaintStyle.Fill,
                            IsAntialias = true
                        });
                }

                // Draw Name
                if (!string.IsNullOrWhiteSpace(target.Name) && DrawConfig.UseRoundedCorners)
                {
                    LabelTextHelper.DrawCenteredText(
                        canvas,
                        target.Name,
                        box.X + box.Width / 2,
                        box.Y - 5,
                        OverlayColor.Text,
                        13
                    );
                }
            }
        }

        /// <summary>
        /// Updates the current target list.
        /// </summary>
        public void SetTargets(List<TargetData> detectedTargets)
        {
            Targets = detectedTargets ?? new();
        }
    }
}
