// Neural v3.1
using SkiaSharp;

namespace JaysAi.Finale.Utility
{
    public static class SkiaSharpExtensions
    {
        public static void DrawTextCentered(this SKCanvas canvas, string text, float x, float y, SKPaint paint)
        {
            var bounds = new SKRect();
            paint.MeasureText(text, ref bounds);
            canvas.DrawText(text, x - bounds.MidX, y - bounds.MidY, paint);
        }

        public static void DrawOutlinedText(this SKCanvas canvas, string text, float x, float y, SKColor textColor, SKColor outlineColor, float size = 20)
        {
            using var outlinePaint = new SKPaint
            {
                Color = outlineColor,
                IsAntialias = true,
                TextSize = size,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2
            };

            using var fillPaint = new SKPaint
            {
                Color = textColor,
                IsAntialias = true,
                TextSize = size,
                Style = SKPaintStyle.Fill
            };

            canvas.DrawText(text, x, y, outlinePaint);
            canvas.DrawText(text, x, y, fillPaint);
        }

        public static void DrawBox(this SKCanvas canvas, SKRect rect, SKColor color, float strokeWidth = 2f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = strokeWidth,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            canvas.DrawRect(rect, paint);
        }
    }
}
