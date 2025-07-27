// Neural v3.1 — SkiaUtils.cs
using SkiaSharp;
using System;

namespace JaysAi.Finale.Overlay.Helpers
{
    public static class SkiaUtils
    {
        public static SKColor ToSkColor(string hex, SKColor fallback)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hex))
                    return fallback;

                if (hex.StartsWith("#"))
                    hex = hex[1..];

                if (hex.Length == 6)
                    return SKColor.Parse("#FF" + hex); // Add full alpha

                return SKColor.Parse("#" + hex);
            }
            catch
            {
                return fallback;
            }
        }

        public static SKColor FadeColor(SKColor color, byte alpha)
        {
            return new SKColor(color.Red, color.Green, color.Blue, alpha);
        }

        public static SKRect Inflate(SKRect rect, float amount)
        {
            return new SKRect(
                rect.Left - amount,
                rect.Top - amount,
                rect.Right + amount,
                rect.Bottom + amount
            );
        }

        public static void DrawCenteredText(SKCanvas canvas, string text, float x, float y, SKPaint paint)
        {
            var bounds = new SKRect();
            paint.MeasureText(text, ref bounds);
            float textX = x - bounds.MidX;
            float textY = y - bounds.MidY;
            canvas.DrawText(text, textX, textY, paint);
        }
    }
}
