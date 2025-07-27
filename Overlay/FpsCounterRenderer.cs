// Neural v3.1 — FpsCounterRenderer.cs
using JaysAi.Finale.Settings;
using SkiaSharp;
using System;
using System.Diagnostics;

namespace JaysAi.Finale.Overlay
{
    public class FpsCounterRenderer : IOverlayRenderer
    {
        public bool IsActive { get; set; } = true;

        private int _frameCount;
        private Stopwatch _stopwatch = Stopwatch.StartNew();
        private float _fps;

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || !UserSettings.Instance.Get("ShowFpsCounter", false))
                return;

            _frameCount++;
            if (_stopwatch.ElapsedMilliseconds >= 1000)
            {
                _fps = _frameCount * 1000f / _stopwatch.ElapsedMilliseconds;
                _frameCount = 0;
                _stopwatch.Restart();
            }

            var paint = new SKPaint
            {
                Color = UserSettings.Instance.Get("FpsColor", SKColors.Lime),
                TextSize = UserSettings.Instance.Get("FpsFontSize", 20f),
                IsAntialias = true
            };

            canvas.DrawText($"FPS: {MathF.Round(_fps)}", 10, 25, paint);
        }
    }
}
