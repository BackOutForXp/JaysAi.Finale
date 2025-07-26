// Neural v3.0 — LabelTextHelper.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public static class LabelTextHelper
    {
        /// <summary>
        /// Draws text at a specific screen position with default style.
        /// </summary>
        public static void DrawText(SKCanvas canvas, string text, float x, float y, SKColor? color = null, float textSize = 14f)
        {
            if (string.IsNullOrWhiteSpace(text) || canvas == null)
                return;

            using var paint = new SKPaint
            {
                Color = color ?? SKColors.White,
                IsAntialias = true,
                TextSize = textSize,
                Typeface = SKTypeface.FromFamilyName("Consolas"),
                IsStroke = false,
                Style = SKPaintStyle.Fill
            };

            canvas.DrawText(text, x, y, paint);
        }

        /// <summary>
        /// Draws centered text above a box or object.
        /// </summary>
        public static void DrawCenteredText(SKCanvas canvas, string text, float centerX, float topY, SKColor? color = null, float textSize = 14f)
        {
            if (string.IsNullOrWhiteSpace(text) || canvas == null)
                return;

            using var paint = new SKPaint
            {
                Color = color ?? SKColors.White,
                IsAntialias = true,
                TextSize = textSize,
                Typeface = SKTypeface.FromFamilyName("Consolas"),
                IsStroke = false,
                Style = SKPaintStyle.Fill
            };

            float textWidth = paint.MeasureText(text);
            float x = centerX - (textWidth / 2);

            canvas.DrawText(text, x, topY, paint);
        }
    }
}
