//monarch v2.1
using System;
using System.Numerics;
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public class ESPObject
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public SKRect BoundingBox { get; set; }
        public float Confidence { get; set; }
        public Vector2 ScreenPosition { get; set; }
        public DateTime Timestamp { get; set; }

        public void Draw(SKCanvas canvas, ESPStyleConfig style)
        {
            if (canvas == null || style == null) return;

            using var paint = new SKPaint
            {
                Color = style.BoxColor,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = style.BoxThickness,
                IsAntialias = true
            };

            using var labelPaint = new SKPaint
            {
                Color = style.TextColor,
                TextSize = style.TextSize,
                IsAntialias = true,
                Typeface = SKTypeface.Default
            };

            canvas.DrawRect(BoundingBox, paint);
            canvas.DrawText(Label, BoundingBox.Left, BoundingBox.Top - 4, labelPaint);
        }
    }
}
