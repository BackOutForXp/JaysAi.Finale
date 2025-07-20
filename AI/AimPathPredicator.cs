//monarch v2.1
using System;
using JaysAi.AI.Models;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.AI
{
    public class AimPathPredictor
    {
        private readonly IRenderBackend renderBackend;
        private readonly float predictionMultiplier;

        public AimPathPredictor(IRenderBackend backend, float multiplier = 1.0f)
        {
            renderBackend = backend;
            predictionMultiplier = multiplier;
        }

        public void DrawPredictionLine(TargetData target)
        {
            if (target == null) return;

            float predictedX = target.CenterX + target.VelocityX * predictionMultiplier;
            float predictedY = target.CenterY + target.VelocityY * predictionMultiplier;

            renderBackend.DrawCircle(predictedX, predictedY, 0.01f); // Small dot
            renderBackend.DrawLine(target.CenterX, target.CenterY, predictedX, predictedY);
        }
    }
}
