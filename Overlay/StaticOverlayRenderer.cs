// Neural v3.1 — StaticOverlayRenderer.cs
using JaysAi.Finale.Overlay.Interfaces;
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public class StaticOverlayRenderer : IOverlayRenderer
    {
        public bool IsActive { get; set; } = true;

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            using var paint = new SKPaint
            {
                Color = SKColors.Green,
                IsAntialias = true,
                TextSize = 16
            };

            canvas.DrawText("JaysAi Neural Overlay Active", 20, 30, paint);
        }
    }
}
