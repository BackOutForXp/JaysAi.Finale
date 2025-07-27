// Neural v3.1 — DebugOverlayRenderer.cs
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public class DebugOverlayRenderer : IOverlayRenderer
    {
        public bool IsActive { get; set; } = true;

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || !UserSettings.Instance.Get("DebugOverlayEnabled", false))
                return;

            var targets = TargetCache.Current; // assume populated by AI scan

            if (targets == null || targets.Count == 0)
                return;

            foreach (var target in targets)
            {
                if (!target.IsVisible || target.ScreenPosition == null)
                    continue;

                var pos = target.ScreenPosition.Value;
                var id = target.Id.ToString();

                using var paint = new SKPaint
                {
                    IsAntialias = true,
                    TextSize = 16,
                    Color = SKColors.Yellow
                };

                canvas.DrawText($"[{id}] {target.Distance:F1}m", pos.X + 8, pos.Y, paint);
            }
        }
    }
}
