// File: Utility\AimSmoother.cs

using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public static class AimSmoother
    {
        /// <summary>
        /// Applies simple linear smoothing between current and target.
        /// </summary>
        /// <param name="current">Current aim position</param>
        /// <param name="target">Target aim position</param>
        /// <param name="smoothing">Smoothing factor. 0 = instant snap</param>
        /// <returns>New aim position</returns>
        public static Vector2 Apply(Vector2 current, Vector2 target, float smoothing)
        {
            if (smoothing <= 0f)
                return target;

            Vector2 delta = target - current;
            return current + delta / smoothing;
        }
    }
}
