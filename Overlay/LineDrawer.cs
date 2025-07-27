// Neural v3.1 — LineDrawer.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public static class LineDrawer
    {
        public static void DrawLine(SKCanvas canvas, float x1, float y1, float x2, float y2, SKColor color, float thickness = 1f)
        {
            using var paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = thickness,
                Color = color
            };

            canvas.DrawLine(x1, y1, x2, y2, paint);
        }
    }
}
