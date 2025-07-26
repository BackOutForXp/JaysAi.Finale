// neural v3.0
using JaysAi.Finale.Data;
using System.Numerics;
using static Unity.Storage.RegistrationSet;

namespace JaysAi.Finale.Aim
{
    public static class PredictionAid
    {
        /// <summary>
        /// Calculates a predicted future position of a moving enemy.
        /// </summary>
        public static Vector3 PredictTargetPosition(Entity enemy, Vector3 shooterPos, float bulletSpeed)
        {
            if (enemy == null || bulletSpeed <= 0f)
                return enemy?.Position ?? shooterPos;

            Vector3 displacement = enemy.Position - shooterPos;
            float distance = displacement.Length();

            float travelTime = distance / bulletSpeed;

            return enemy.Position + (enemy.Velocity * travelTime);
        }

        /// <summary>
        /// Predicts position in 2D space for ESP or flat overlays.
        /// </summary>
        public static Vector2 Predict2D(Vector2 currentPos, Vector2 velocity, float deltaTime)
        {
            return currentPos + (velocity * deltaTime);
        }

        /// <summary>
        /// Basic linear extrapolation.
        /// </summary>
        public static Vector3 LinearExtrapolate(Vector3 origin, Vector3 velocity, float delta)
        {
            return origin + (velocity * delta);
        }
    }
}
