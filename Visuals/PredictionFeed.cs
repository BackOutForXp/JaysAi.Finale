//monarch v2.1
using System;
using System.Collections.Generic;
using SkiaSharp;
using JaysAi.AI;

namespace JaysAi.Finale.Visuals
{
    public static class PredictionFeed
    {
        public static List<string> LatestLabels { get; private set; } = new();
        public static List<float> LatestConfidences { get; private set; } = new();

        public static void UpdatePredictions(List<PredictionResult> predictions)
        {
            LatestLabels.Clear();
            LatestConfidences.Clear();

            foreach (var prediction in predictions)
            {
                LatestLabels.Add(prediction.Label);
                LatestConfidences.Add(prediction.Confidence);
            }
        }

        public static void Draw(SKCanvas canvas)
        {
            if (LatestLabels.Count == 0 || canvas == null) return;

            using var paint = new SKPaint
            {
                Color = SKColors.LimeGreen,
                TextSize = 24,
                IsAntialias = true,
                Typeface = SKTypeface.Default
            };

            float x = 20;
            float y = 40;

            for (int i = 0; i < LatestLabels.Count; i++)
            {
                string label = LatestLabels[i];
                float confidence = LatestConfidences[i];
                string displayText = $"{label}: {confidence:P0}";

                canvas.DrawText(displayText, x, y, paint);
                y += 30;
            }
        }
    }
}
