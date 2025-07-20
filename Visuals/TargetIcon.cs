//monarch v2.1
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public class TargetIcon
    {
        public float Size { get; set; } = 12f;
        public float Stroke { get; set; } = 2f;

        public void DrawDiamond(SKCanvas canvas, float x, float y, SKColor color)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = Stroke,
                IsAntialias = true,
                IsStroke = true
            };

            var path = new SKPath();
            path.MoveTo(x, y - Size);         // Top
            path.LineTo(x + Size, y);         // Right
            path.LineTo(x, y + Size);         // Bottom
            path.LineTo(x - Size, y);         // Left
            path.Close();

            canvas.DrawPath(path, paint);
        }

        public void DrawCrosshair(SKCanvas canvas, float x, float y, SKColor color)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = Stroke,
                IsAntialias = true
            };

            canvas.DrawLine(x - Size, y, x + Size, y, paint);
            canvas.DrawLine(x, y - Size, x, y + Size, paint);
        }

        public void DrawRing(SKCanvas canvas, float x, float y, float radius, SKColor color)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = Stroke,
                IsAntialias = true,
                IsStroke = true
            };

            canvas.DrawCircle(x, y, radius, paint);
        }
    }
}
