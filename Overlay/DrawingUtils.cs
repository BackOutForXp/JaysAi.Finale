using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public static class DrawingUtils
    {
        public static void DrawBox(SKCanvas canvas, float x, float y, float width, float height, SKColor color, float stroke = 2)
        {
            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = color,
                StrokeWidth = stroke,
                IsAntialias = true
            };

            canvas.DrawRect(x, y, width, height, paint);
        }

        public static void DrawLine(SKCanvas canvas, float x1, float y1, float x2, float y2, SKColor color, float stroke = 1)
        {
            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = color,
                StrokeWidth = stroke,
                IsAntialias = true
            };

            canvas.DrawLine(x1, y1, x2, y2, paint);
        }

        public static void DrawCircle(SKCanvas canvas, float x, float y, float radius, SKColor color, float stroke = 2)
        {
            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = color,
                StrokeWidth = stroke,
                IsAntialias = true
            };

            canvas.DrawCircle(x, y, radius, paint);
        }

        public static void DrawText(SKCanvas canvas, string text, float x, float y, float size, SKColor color)
        {
            using var paint = new SKPaint
            {
                Color = color,
                TextSize = size,
                IsAntialias = true,
                IsStroke = false
            };

            canvas.DrawText(text, x, y, paint);
        }

        public static void DrawFilledBox(SKCanvas canvas, float x, float y, float width, float height, SKColor fillColor)
        {
            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = fillColor,
                IsAntialias = true
            };

            canvas.DrawRect(x, y, width, height, paint);
        }
    }
}
