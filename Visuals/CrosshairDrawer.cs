//monarch v2.1
using SkiaSharp;
using JaysAi.Aim;

namespace JaysAi.Finale.Visuals
{
    public static class CrosshairDrawer
    {
        public static bool IsVisible { get; set; } = true;
        public static SKColor CrosshairColor { get; set; } = SKColors.Red;
        public static int Size { get; set; } = 10;
        public static int Thickness { get; set; } = 2;

        public static void Draw(SKCanvas canvas, SKPoint center)
        {
            if (!IsVisible || canvas == null) return;

            using var paint = new SKPaint
            {
                Color = CrosshairColor,
                StrokeWidth = Thickness,
                IsAntialias = true
            };

            // Horizontal line
            canvas.DrawLine(
                center.X - Size,
                center.Y,
                center.X + Size,
                center.Y,
                paint
            );

            // Vertical line
            canvas.DrawLine(
                center.X,
                center.Y - Size,
                center.X,
                center.Y + Size,
                paint
            );
        }
    }
}
