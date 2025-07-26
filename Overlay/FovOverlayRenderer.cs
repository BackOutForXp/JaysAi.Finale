// Neural v3.1 — FovOverlayRenderer.cs
using SkiaSharp;
using System;

namespace JaysAi.Finale.Overlay
{
    public class FovOverlayRenderer
    {
        public bool IsEnabled { get; set; } = true;
        public float Radius { get; set; } = 120f;
        public SKColor Color { get; set; } = new SKColor(255, 255, 0, 180); // Yellow
        public float Thickness { get; set; } = 2f;
        public bool FillEnabled { get; set; } = false;
        public SKColor FillColor { get; set; } = new SKColor(255, 255, 0, 40); // Transparent yellow

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsEnabled || canvas == null) return;

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;

            // Fill circle
            if (FillEnabled)
            {
                using var fillPaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = FillColor,
                    IsAntialias = true
                };

                canvas.DrawCircle(centerX, centerY, Radius, fillPaint);
            }

            // Border circle
            using var borderPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = Thickness,
                Color = Color,
                IsAntialias = true
            };

            canvas.DrawCircle(centerX, centerY, Radius, borderPaint);
        }
    }
}
