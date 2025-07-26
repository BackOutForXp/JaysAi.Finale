// Neural v3.0 — BoxRenderer.cs
using System;
using System.Collections.Generic;
using SkiaSharp;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.Overlay
{
    public class BoxRenderer
    {
        private readonly SKPaint _boxPaint;
        private readonly SKPaint _outlinePaint;

        public BoxRenderer()
        {
            _boxPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2,
                Color = SKColors.Red,
                IsAntialias = true
            };

            _outlinePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 4,
                Color = SKColors.Black.WithAlpha(200),
                IsAntialias = true
            };
        }

        public void Render(SKCanvas canvas, IList<DetectedObject> targets)
        {
            if (canvas == null || targets == null || targets.Count == 0)
                return;

            foreach (var target in targets)
            {
                if (!target.IsValid) continue;

                var rect = new SKRect(
                    target.ScreenPositionX,
                    target.ScreenPositionY,
                    target.ScreenPositionX + target.Width,
                    target.ScreenPositionY + target.Height
                );

                // Draw outline for better visibility
                canvas.DrawRect(rect, _outlinePaint);
                canvas.DrawRect(rect, _boxPaint);
            }
        }

        public void SetBoxColor(SKColor color)
        {
            _boxPaint.Color = color;
        }

        public void SetStrokeWidth(float width)
        {
            _boxPaint.StrokeWidth = width;
            _outlinePaint.StrokeWidth = width + 2;
        }
    }
}
