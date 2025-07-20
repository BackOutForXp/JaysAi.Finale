//monarch v2.1 – Drawing Utility Toolkit for Overlay
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public static class DrawUtils
    {
        public static void DrawBox(SKCanvas canvas, SKRect rect, SKColor color, float stroke = 2f)
        {
            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = color,
                StrokeWidth = stroke,
                IsAntialias = true
            };
            canvas.DrawRect(rect, paint);
        }

        public static void DrawFilledBox(SKCanvas canvas, SKRect rect, SKColor color)
        {
            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = color,
                IsAntialias = true
            };
            canvas.DrawRect(rect, paint);
        }

        public static void DrawCircle(SKCanvas canvas, SKPoint center, float radius, SKColor color, float stroke = 2f)
        {
            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = color,
                StrokeWidth = stroke,
                IsAntialias = true
            };
            canvas.DrawCircle(center, radius, paint);
        }

        public static void DrawSnapline(SKCanvas canvas, SKPoint from, SKPoint to, SKColor color, float stroke = 1.5f)
        {
            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = color,
                StrokeWidth = stroke,
                IsAntialias = true
            };
            canvas.DrawLine(from, to, paint);
        }

        public static void DrawText(SKCanvas canvas, string text, float x, float y, float size, SKColor color)
        {
            using var paint = new SKPaint
            {
                Color = color,
                TextSize = size,
                IsAntialias = true,
                IsStroke = false,
                Typeface = SKTypeface.Default
            };
            canvas.DrawText(text, x, y, paint);
        }
    }
}
