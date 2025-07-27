// Neural v3.1 — TargetBoxRenderer.cs
using JaysAi.Finale.Data;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public class TargetBoxRenderer : IOverlayRenderer
    {
        public bool IsActive { get; set; } = true;

        private List<DetectedObject> _targets = new();

        public void SetTargets(List<DetectedObject> targets)
        {
            _targets = targets;
        }

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || _targets == null || _targets.Count == 0)
                return;

            using var paint = new SKPaint
            {
                IsAntialias = true,
                StrokeWidth = UserSettings.Instance.Get("esp.stroke", 2f),
                Color = UserSettings.Instance.Get("esp.color", SKColors.Lime),
                Style = SKPaintStyle.Stroke
            };

            foreach (var target in _targets)
            {
                if (!target.IsVisible || target.ScreenBox == null)
                    continue;

                var box = target.ScreenBox.Value;

                canvas.DrawRect(box.X, box.Y, box.Width, box.Height, paint);
            }
        }
    }
}
