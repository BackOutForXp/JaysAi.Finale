// Neural v3.0 — CrosshairDrawer.cs
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;

namespace JaysAi.Finale.Overlay
{
    public class CrosshairDrawer
    {
        public bool IsEnabled { get; set; } = true;
        public float Size { get; set; } = 10f;
        public float Thickness { get; set; } = 2f;
        public SKColor Color { get; set; } = new SKColor(255, 0, 0, 200); // Red with alpha
        public CrosshairStyle Style { get; set; } = CrosshairStyle.Plus;

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsEnabled || canvas == null) return;

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;

            using var paint = new SKPaint
            {
                Color = Color,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = Thickness
            };

            switch (Style)
            {
                case CrosshairStyle.Plus:
                    DrawPlusCrosshair(canvas, paint, centerX, centerY);
                    break;

                case CrosshairStyle.Circle:
                    canvas.DrawCircle(centerX, centerY, Size, paint);
                    break;

                case CrosshairStyle.Dot:
                    paint.Style = SKPaintStyle.Fill;
                    canvas.DrawCircle(centerX, centerY, Thickness, paint);
                    break;

                case CrosshairStyle.T:
                    DrawTCrosshair(canvas, paint, centerX, centerY);
                    break;

                default:
                    DrawPlusCrosshair(canvas, paint, centerX, centerY);
                    break;
            }
        }

        private void DrawPlusCrosshair(SKCanvas canvas, SKPaint paint, float x, float y)
        {
            canvas.DrawLine(x - Size, y, x + Size, y, paint); // Horizontal
            canvas.DrawLine(x, y - Size, x, y + Size, paint); // Vertical
        }

        private void DrawTCrosshair(SKCanvas canvas, SKPaint paint, float x, float y)
        {
            canvas.DrawLine(x - Size, y, x + Size, y, paint); // Horizontal
            canvas.DrawLine(x, y, x, y + Size, paint);         // Only downward vertical
        }
    }

    public enum CrosshairStyle
    {
        Plus,
        Circle,
        Dot,
        T
    }
}
