// Neural v3.0 — OverlayShape.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public static class OverlayShape
    {
        /// <summary>
        /// Draws a rectangular box (outline only) with thickness.
        /// </summary>
        public static void DrawBox(SKCanvas canvas, float x, float y, float width, float height, SKColor color, float thickness = 2f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = thickness,
                IsAntialias = true
            };

            var rect = new SKRect(x, y, x + width, y + height);
            canvas.DrawRect(rect, paint);
        }

        /// <summary>
        /// Draws a filled rectangle (e.g., health bar or background).
        /// </summary>
        public static void FillBox(SKCanvas canvas, float x, float y, float width, float height, SKColor fillColor)
        {
            using var paint = new SKPaint
            {
                Color = fillColor,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            var rect = new SKRect(x, y, x + width, y + height);
            canvas.DrawRect(rect, paint);
        }

        /// <summary>
        /// Draws a smooth circle or ring (e.g., FOV).
        /// </summary>
        public static void DrawCircle(SKCanvas canvas, float centerX, float centerY, float radius, SKColor color, float thickness = 2f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = thickness,
                IsAntialias = true
            };

            canvas.DrawCircle(centerX, centerY, radius, paint);
        }

        /// <summary>
        /// Draws a solid line between two points.
        /// </summary>
        public static void DrawLine(SKCanvas canvas, float startX, float startY, float endX, float endY, SKColor color, float thickness = 1f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = thickness,
                IsAntialias = true
            };

            canvas.DrawLine(startX, startY, endX, endY, paint);
        }

        /// <summary>
        /// Draws a filled ring or donut (for indicators or overlays).
        /// </summary>
        public static void DrawRing(SKCanvas canvas, float centerX, float centerY, float radius, float thickness, SKColor color)
        {
            using var paint = new SKPaint
            {
                Color = color,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = thickness,
                IsAntialias = true
            };

            canvas.DrawCircle(centerX, centerY, radius, paint);
        }
    }
}
