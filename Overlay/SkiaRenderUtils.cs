// Neural v3.1 — SkiaRenderUtils.cs
using SkiaSharp;
using System;

namespace JaysAi.Finale.Overlay
{
    public static class SkiaRenderUtils
    {
        public static void DrawCenteredText(SKCanvas canvas, string text, float x, float y, float fontSize, SKColor color, string fontName = "Arial")
        {
            using var paint = new SKPaint
            {
                Color = color,
                TextSize = fontSize,
                IsAntialias = true,
                Typeface = SkiaFontCache.GetTypeface(fontName)
            };

            var bounds = new SKRect();
            paint.MeasureText(text, ref bounds);

            float textX = x - bounds.MidX;
            float textY = y - bounds.MidY;

            canvas.DrawText(text, textX, textY, paint);
        }

        public static void DrawLine(SKCanvas canvas, float x1, float y1, float x2, float y2, SKColor color, float thickness = 2f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = thickness,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            canvas.DrawLine(x1, y1, x2, y2, paint);
        }

        public static void DrawCircle(SKCanvas canvas, float centerX, float centerY, float radius, SKColor color, float thickness = 2f)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = thickness,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            canvas.DrawCircle(centerX, centerY, radius, paint);
        }

        public static void DrawFilledCircle(SKCanvas canvas, float centerX, float centerY, float radius, SKColor fillColor)
        {
            using var paint = new SKPaint
            {
                Color = fillColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            };

            canvas.DrawCircle(centerX, centerY, radius, paint);
        }
    }
}
