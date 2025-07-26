// Neural v3.0 — OverlayShape.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public static class OverlayShape
    {
        /// <summary>
        /// Draws a basic rectangle.
        /// </summary>
        public static void DrawBox(SKCanvas canvas, float x, float y, float width, float height, SKColor color, float stroke = 2f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = stroke,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            canvas.DrawRect(x, y, width, height, paint);
        }

        /// <summary>
        /// Draws a filled rectangle behind other visuals.
        /// </summary>
        public static void DrawFilledBox(SKCanvas canvas, float x, float y, float width, float height, SKColor fillColor)
        {
            using var paint = new SKPaint
            {
                Color = fillColor,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            canvas.DrawRect(x, y, width, height, paint);
        }

        /// <summary>
        /// Draws a circle or FOV ring.
        /// </summary>
        public static void DrawCircle(SKCanvas canvas, float centerX, float centerY, float radius, SKColor color, float stroke = 2f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = stroke,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            canvas.DrawCircle(centerX, centerY, radius, paint);
        }

        /// <summary>
        /// Draws a line from point A to B.
        /// </summary>
        public static void DrawLine(SKCanvas canvas, float x1, float y1, float x2, float y2, SKColor color, float thickness = 1f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = thickness,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            canvas.DrawLine(x1, y1, x2, y2, paint);
        }

        /// <summary>
        /// Draws a text label near an object or inside a box.
        /// </summary>
        public static void DrawLabel(SKCanvas canvas, string text, float x, float y, SKColor color, float size = 14f)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            using var paint = new SKPaint
            {
                Color = color,
                TextSize = size,
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName("Consolas"),
                Style = SKPaintStyle.Fill
            };

            canvas.DrawText(text, x, y, paint);
        }
    }
}
