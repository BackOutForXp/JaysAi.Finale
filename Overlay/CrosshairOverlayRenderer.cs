// Neural v3.1 — CrosshairOverlayRenderer.cs
using JaysAi.Finale.Overlay.Interfaces;
using JaysAi.Finale.Settings;
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public class CrosshairOverlayRenderer : IOverlayRenderer
    {
        public bool IsActive { get; set; } = true;

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || !UserSettings.Instance.Get("CrosshairEnabled", true))
                return;

            float size = UserSettings.Instance.Get("CrosshairSize", 10f);
            float thickness = UserSettings.Instance.Get("CrosshairThickness", 1f);
            var color = UserSettings.Instance.Get("CrosshairColor", SKColors.Red);

            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = thickness,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            float centerX = screenWidth / 2f;
            float centerY = screenHeight / 2f;

            // Horizontal line
            canvas.DrawLine(centerX - size, centerY, centerX + size, centerY, paint);

            // Vertical line
            canvas.DrawLine(centerX, centerY - size, centerX, centerY + size, paint);
        }
    }
}
