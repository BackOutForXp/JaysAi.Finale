using System.Collections.Generic;
using System.Numerics;
using SkiaSharp;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Debugging
{
    public static class PredictionVisualizer
    {
        public static bool Enabled { get; set; } = true;
        public static SKColor PredictionLineColor = new SKColor(0, 255, 255, 160); // cyan
        public static SKColor RawLineColor = new SKColor(255, 0, 0, 120); // red

        public static void Draw(SKCanvas canvas, List<Enemy> enemies, float predictionTime)
        {
            if (!Enabled || enemies == null) return;

            foreach (var enemy in enemies)
            {
                if (!enemy.IsVisible || enemy.ScreenPosition == null) continue;

                var currentPos = enemy.Position;
                var velocity = enemy.Velocity;
                var predictedPos = currentPos + velocity * predictionTime;

                if (!WorldToScreenConverter.TryProject(predictedPos, out var predictedScreen)) continue;

                // Draw line from current screen pos to predicted screen pos
                using var predictedPaint = new SKPaint
                {
                    Color = PredictionLineColor,
                    StrokeWidth = 2,
                    IsAntialias = true
                };

                using var rawPaint = new SKPaint
                {
                    Color = RawLineColor,
                    StrokeWidth = 1,
                    IsAntialias = true,
                    PathEffect = SKPathEffect.CreateDash(new float[] { 6, 4 }, 0)
                };

                SKPoint from = enemy.ScreenPosition.Value;
                SKPoint to = predictedScreen;

                // Line: actual position → predicted future position
                canvas.DrawLine(from, to, predictedPaint);

                // Optional: dot or small circle at end
                canvas.DrawCircle(to, 3f, rawPaint);
            }
        }
    }
}
