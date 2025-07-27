// Neural v3.1
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public class AiOverlay
    {
        private Ai.AiManager? _aiManager;

        public void BindToAI(Ai.AiManager manager)
        {
            _aiManager = manager;
        }

        public void Unbind()
        {
            _aiManager = null;
        }

        public void UpdateOverlayData(List<TrackedTarget> targets)
        {
            // Future extension: if target overlay should be interactive, extend here.
        }

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (_aiManager == null || !UserSettings.Instance.Get("OverlayAiDebug", false))
                return;

            var paint = new SKPaint
            {
                Color = SKColors.Yellow,
                IsAntialias = true,
                TextSize = 14
            };

            var targets = _aiManager.GetCurrentTargets();
            int offsetY = 20;

            foreach (var target in targets)
            {
                if (!target.IsVisible || target.ScreenBox == null)
                    continue;

                var box = target.ScreenBox.Value;
                string text = $"ID: {target.Id}  FOV: {target.FovDistance:F1}  Conf: {target.Confidence:P0}";

                canvas.DrawText(text, box.X, box.Y - offsetY, paint);
            }
        }
    }
}
