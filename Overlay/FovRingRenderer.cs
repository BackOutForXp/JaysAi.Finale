// Neural v3.0 — FovRingRenderer.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public class FovRingRenderer
    {
        public bool IsActive { get; set; } = true;

        public float Radius { get; set; } = 100f;
        public float StrokeThickness { get; set; } = 2f;
        public SKColor RingColor { get; set; } = new SKColor(0, 255, 0, 200); // Green
        public bool Dashed { get; set; } = false;

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || canvas == null) return;

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;

            using var paint = new SKPaint
            {
                IsAntialias = true,
                Color = RingColor,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = StrokeThickness
            };

            if (Dashed)
            {
                paint.PathEffect = SKPathEffect.CreateDash(new float[] { 10f, 10f }, 0);
            }

            canvas.DrawCircle(centerX, centerY, Radius, paint);
        }

        public void Toggle() => IsActive = !IsActive;

        public void SetRadius(float radius) => Radius = radius;

        public void SetColor(SKColor color) => RingColor = color;

        public void SetThickness(float thickness) => StrokeThickness = thickness;

        public void EnableDashed(bool enabled) => Dashed = enabled;
    }
}
