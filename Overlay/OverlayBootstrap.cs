using System.Collections.Generic;
using SkiaSharp;
using JaysAi.Finale.AI;
using JaysAi.Finale.Debugging;
using JaysAi.Finale.Targeting;

namespace JaysAi.Finale.Overlay
{
    public static class OverlayBootstrap
    {
        public static void Render(SKCanvas canvas, List<Enemy> enemies)
        {
            if (canvas == null || enemies == null)
                return;

            // Draw prediction lines
            PredictionVisualizer.Draw(canvas, enemies, predictionTime: 0.25f);

            // Draw heatmap of shot impacts
            TrainingHeatmap.Draw(canvas);

            // Draw ESP bounding boxes or names (optional)
            DrawEnemyLabels(canvas, enemies);
        }

        private static void DrawEnemyLabels(SKCanvas canvas, List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.IsVisible || enemy.ScreenPosition == null)
                    continue;

                using var paint = new SKPaint
                {
                    Color = SKColors.Yellow,
                    TextSize = 16,
                    IsAntialias = true
                };

                var screen = enemy.ScreenPosition.Value;
                canvas.DrawText($"#{enemy.Id}", screen.X + 5, screen.Y - 5, paint);
            }
        }
    }
}
