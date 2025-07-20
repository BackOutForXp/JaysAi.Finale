//monarch v2.1
using System.Collections.Generic;
using SkiaSharp;
using JaysAi.AI;

namespace JaysAi.Finale.Visuals
{
    public static class VisualDebugger
    {
        public static bool EnableBoxes { get; set; } = true;
        public static bool EnableFOV { get; set; } = true;
        public static float FovRadius { get; set; } = 150f;
        public static SKColor BoxColor { get; set; } = SKColors.Cyan;
        public static SKColor FovColor { get; set; } = new SKColor(255, 255, 0, 128); // semi-transparent yellow

        public static void Draw(SKCanvas canvas, List<PredictionResult> entities, SKPoint screenCenter)
        {
            if (canvas == null || entities == null) return;

            if (EnableFOV)
            {
                using var fovPaint = new SKPaint
                {
                    Color = FovColor,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 2,
                    IsAntialias = true
                };

                canvas.DrawCircle(screenCenter, FovRadius, fovPaint);
            }

            if (!EnableBoxes) return;

            using var boxPaint = new SKPaint
            {
                Color = BoxColor,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2,
                IsAntialias = true
            };

            foreach (var entity in entities)
            {
                if (entity == null || !entity.IsOnScreen) continue;

                var rect = new SKRect(
                    entity.BoundingBox.Left,
                    entity.BoundingBox.Top,
                    entity.BoundingBox.Right,
                    entity.BoundingBox.Bottom
                );

                canvas.DrawRect(rect, boxPaint);
            }
        }
    }
}
