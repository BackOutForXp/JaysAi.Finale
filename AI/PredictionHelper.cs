// File: Helpers/PredictionHelper.cs
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public static class PredictionHelper
    {
        /// <summary>
        /// Predicts the impact point for a moving enemy assuming linear motion.
        /// </summary>
        /// <param name="enemyPosition">Current enemy position in world space</param>
        /// <param name="enemyVelocity">Enemy's movement vector</param>
        /// <param name="playerPosition">Current player position</param>
        /// <param name="bulletSpeed">Projectile speed in units per second</param>
        /// <returns>Estimated position to aim at</returns>
        public static Vector3 Predict(Vector3 enemyPosition, Vector3 enemyVelocity, Vector3 playerPosition, float bulletSpeed)
        {
            if (bulletSpeed <= 0) return enemyPosition;

            Vector3 toTarget = enemyPosition - playerPosition;
            float distance = toTarget.Length();
            float timeToHit = distance / bulletSpeed;

            Vector3 predictedOffset = enemyVelocity * timeToHit;

            return enemyPosition + predictedOffset;
        }
    }
}
