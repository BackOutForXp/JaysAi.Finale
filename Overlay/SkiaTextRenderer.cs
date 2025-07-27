// Neural v3.1 — SkiaTextRenderer.cs
using SkiaSharp;
using System;

namespace JaysAi.Finale.Overlay
{
    public static class SkiaTextRenderer
    {
        public static void DrawText(
            SKCanvas canvas,
            string text,
            float x,
            float y,
            SKColor color,
            float textSize = 14f,
            string fontName = "Arial",
            SKTextAlign align = SKTextAlign.Center)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            using var paint = new SKPaint
            {
                Color = color,
                TextSize = textSize,
                Typeface = SkiaFontCache.GetTypeface(fontName),
                IsAntialias = true,
                TextAlign = align,
                IsStroke = false
            };

            canvas.DrawText(text, x, y, paint);
        }

        public static void DrawShadowedText(
            SKCanvas canvas,
            string text,
            float x,
            float y,
            SKColor textColor,
            SKColor shadowColor,
            float textSize = 14f,
            float shadowOffset = 1.5f,
            string fontName = "Arial")
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            // Draw shadow
            DrawText(canvas, text, x + shadowOffset, y + shadowOffset, shadowColor, textSize, fontName);

            // Draw main text
            DrawText(canvas, text, x, y, textColor, textSize, fontName);
        }
    }
}
