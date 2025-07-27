// Neural v3.1 — FovOverlayRenderer.cs
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public class FovOverlayRenderer : IOverlayRenderer
    {
        public bool IsActive { get; set; } = true;

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || !UserSettings.Instance.Get("FovOverlayEnabled", true))
                return;

            float fovRadius = UserSettings.Instance.Get("FovRadius", 120f);
            float thickness = UserSettings.Instance.Get("FovThickness", 1.5f);
            var color = UserSettings.Instance.Get("FovColor", SKColors.Orange);

            using var paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = thickness,
                Color = color
            };

            float cx = screenWidth / 2f;
            float cy = screenHeight / 2f;

            canvas.DrawCircle(cx, cy, fovRadius, paint);
        }
    }
}
