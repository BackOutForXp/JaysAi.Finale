using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public static class PredictionHelper
    {
        /// <summary>
        /// Predicts future enemy position using velocity and deltaTime.
        /// </summary>
        public static Vector3 Predict(Enemy enemy, float deltaTime)
        {
            if (!enemy.IsAlive || enemy.Position == default || enemy.Velocity == Vector3.Zero)
                return enemy.Position;

            return enemy.Position + (enemy.Velocity * deltaTime);
        }

        /// <summary>
        /// Predicts screen position assuming W2S converter is injected.
        /// </summary>
        public static bool TryPredictScreen(Enemy enemy, float deltaTime, IWorldToScreen w2s, out Vector2 screenPos)
        {
            screenPos = default;

            var predicted = Predict(enemy, deltaTime);
            return w2s.TryProject(predicted, out screenPos);
        }
    }
}
