// File: Visuals/FovRingRenderer.cs
using SkiaSharp;
using System.Numerics;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Visuals
{
    public static class FovRingRenderer
    {
        public static void Draw(SKCanvas canvas, Vector2 screenCenter, AppSettings settings)
        {
            if (!settings.EnableFovRing || canvas == null) return;

            float radius = settings.FovLimit;
            var paint = new SKPaint
            {
                Color = SKColors.Cyan,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2,
                IsAntialias = true
            };

            canvas.DrawCircle(screenCenter.X, screenCenter.Y, radius, paint);
        }
    }
}
