// Neural v3.1 — CrosshairRenderer.cs
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public class CrosshairRenderer : IOverlayRenderer
    {
        public bool IsActive { get; set; } = true;

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || !UserSettings.Instance.Get("CrosshairEnabled", true))
                return;

            float size = UserSettings.Instance.Get("CrosshairSize", 8f);
            float thickness = UserSettings.Instance.Get("CrosshairThickness", 1f);
            var color = UserSettings.Instance.Get("CrosshairColor", SKColors.LimeGreen);

            using var paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = thickness,
                Color = color
            };

            float cx = screenWidth / 2f;
            float cy = screenHeight / 2f;

            // Draw crosshair (simple + pattern)
            canvas.DrawLine(cx - size, cy, cx + size, cy, paint);
            canvas.DrawLine(cx, cy - size, cx, cy + size, paint);
        }
    }
}
