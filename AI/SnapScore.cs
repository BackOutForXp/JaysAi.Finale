// File: Helpers/SnapScore.cs
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public static class SnapScore
    {
        /// <summary>
        /// Scores a potential target based on distance and velocity alignment.
        /// Higher score = more ideal target.
        /// </summary>
        /// <param name="playerPos">Player's current position (world)</param>
        /// <param name="enemyPos">Enemy's position (world)</param>
        /// <param name="enemyVelocity">Enemy's current velocity vector</param>
        /// <returns>Score: higher is better</returns>
        public static float Calculate(Vector3 playerPos, Vector3 enemyPos, Vector3 enemyVelocity)
        {
            Vector3 toTarget = enemyPos - playerPos;
            float distance = toTarget.Length();
            if (distance < 1f) distance = 1f; // avoid div by zero

            float velocityAlignment = Vector3.Dot(enemyVelocity, Vector3.Normalize(toTarget));
            float score = 1f / distance + velocityAlignment * 0.1f;

            return score;
        }
    }
}
