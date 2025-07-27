// Neural v3.1 — TrackingOverlayRenderer.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Overlay.Interfaces;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System.Linq;

namespace JaysAi.Finale.Overlay
{
    public class TrackingOverlayRenderer : IOverlayRenderer
    {
        private readonly AiManager _aiManager;

        public bool IsActive { get; set; } = true;

        public TrackingOverlayRenderer(AiManager aiManager)
        {
            _aiManager = aiManager;
        }

        public void Render(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || _aiManager == null) return;

            var targets = _aiManager.GetCurrentTargets();
            if (targets == null || targets.Count == 0) return;

            using var paint = new SKPaint
            {
                IsAntialias = true,
                Color = UserSettings.Instance.Get("TrackingOverlayColor", SKColors.Cyan),
                StrokeWidth = 2,
                Style = SKPaintStyle.Stroke
            };

            foreach (var target in targets.Where(t => t.IsVisible && t.ScreenBox.HasValue))
            {
                var box = target.ScreenBox.Value;
                SkiaUtils.DrawCornerBox(canvas, box, paint, cornerLength: 8f);
            }
        }
    }
}
