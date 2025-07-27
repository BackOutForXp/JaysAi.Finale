// Neural v3.1
using JaysAi.Finale.AI;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.Features
{
    public class PredictionPathRenderer : IOverlayRenderer
    {
        private readonly PredictionEngine _predictionEngine;

        public bool IsActive => UserSettings.Instance.Get("PredictionPathEnabled", false);

        public PredictionPathRenderer(PredictionEngine predictionEngine)
        {
            _predictionEngine = predictionEngine;
        }

        public void Draw(SKCanvas canvas, int screenWidth, int screenHeight)
        {
            if (!IsActive || _predictionEngine?.LatestPredictions == null)
                return;

            using var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2f,
                Color = UserSettings.Instance.Get("PredictionPathColor", SKColors.Cyan),
                IsAntialias = true
            };

            foreach (var path in _predictionEngine.LatestPredictions)
            {
                if (path == null || path.ScreenPath == null || path.ScreenPath.Count < 2)
                    continue;

                for (int i = 0; i < path.ScreenPath.Count - 1; i++)
                {
                    var a = path.ScreenPath[i];
                    var b = path.ScreenPath[i + 1];
                    canvas.DrawLine(a.X, a.Y, b.X, b.Y, paint);
                }
            }
        }
    }
}
