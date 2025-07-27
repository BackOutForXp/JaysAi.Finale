using System.Numerics;

namespace JaysAi.Finale.Math
{
    public static class FovCalculator
    {
        /// <summary>
        /// Calculates angle distance between two vectors (screen positions).
        /// </summary>
        public static float GetFovDistance(Vector2 from, Vector2 to)
        {
            return Vector2.Distance(from, to);
        }

        /// <summary>
        /// Returns true if target is within the defined FOV radius from screen center.
        /// </summary>
        public static bool IsWithinFov(Vector2 screenCenter, Vector2 target, float fovRadius)
        {
            return Vector2.Distance(screenCenter, target) <= fovRadius;
        }

        /// <summary>
        /// Calculates normalized delta from center to target (used for aim delta).
        /// </summary>
        public static Vector2 GetDirection(Vector2 screenCenter, Vector2 target)
        {
            var delta = target - screenCenter;
            return Vector2.Normalize(delta);
        }
    }
}
