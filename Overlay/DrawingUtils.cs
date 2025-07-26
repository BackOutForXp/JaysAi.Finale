// neural v3.0
using SkiaSharp;
using System;

namespace JaysAi.Finale.Overlay
{
    public static class DrawingUtils
    {
        public static void DrawText(SKCanvas canvas, string text, float x, float y, SKPaint paint)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            canvas.DrawText(text, x, y, paint);
        }

        public static void DrawBox(SKCanvas canvas, float x, float y, float width, float height, SKPaint paint)
        {
            var rect = new SKRect(x, y, x + width, y + height);
            canvas.DrawRect(rect, paint);
        }

        public static void DrawCircle(SKCanvas canvas, float cx, float cy, float radius, SKPaint paint)
        {
            canvas.DrawCircle(cx, cy, radius, paint);
        }

        public static void DrawLine(SKCanvas canvas, float x0, float y0, float x1, float y1, SKPaint paint)
        {
            canvas.DrawLine(x0, y0, x1, y1, paint);
        }

        public static void DrawCrosshair(SKCanvas canvas, float cx, float cy, float size, SKPaint paint)
        {
            DrawLine(canvas, cx - size, cy, cx + size, cy, paint);
            DrawLine(canvas, cx, cy - size, cx, cy + size, paint);
        }

        public static void DrawBorderedText(SKCanvas canvas, string text, float x, float y, SKPaint foreground, SKPaint background, float borderSize = 1f)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            for (float dx = -borderSize; dx <= borderSize; dx++)
            {
                for (float dy = -borderSize; dy <= borderSize; dy++)
                {
                    canvas.DrawText(text, x + dx, y + dy, background);
                }
            }

            canvas.DrawText(text, x, y, foreground);
        }
    }
}
