// neural v3.0
using System;
using System.Numerics;

namespace JaysAi.Finale.Helpers
{
    public static class ViewpointTranslator
    {
        /// <summary>
        /// Converts a 3D world position into a 2D screen position using a given view-projection matrix and screen size.
        /// </summary>
        /// <param name="worldPosition">The 3D world coordinates to convert.</param>
        /// <param name="viewProjectionMatrix">The combined View * Projection matrix from the game.</param>
        /// <param name="screenWidth">The width of the screen in pixels.</param>
        /// <param name="screenHeight">The height of the screen in pixels.</param>
        /// <param name="screenPosition">The resulting 2D screen coordinates if conversion is successful.</param>
        /// <returns>True if the point is in front of the camera and on screen, otherwise false.</returns>
        public static bool WorldToScreen(Vector3 worldPosition, Matrix4x4 viewProjectionMatrix, int screenWidth, int screenHeight, out Vector2 screenPosition)
        {
            screenPosition = Vector2.Zero;

            Vector4 clipSpacePos = Vector4.Transform(new Vector4(worldPosition, 1.0f), viewProjectionMatrix);

            if (clipSpacePos.W < 0.01f)
                return false;

            // Perform perspective divide
            Vector3 ndc = new Vector3(
                clipSpacePos.X / clipSpacePos.W,
                clipSpacePos.Y / clipSpacePos.W,
                clipSpacePos.Z / clipSpacePos.W
            );

            // Convert NDC to screen coordinates
            screenPosition.X = (ndc.X + 1f) * 0.5f * screenWidth;
            screenPosition.Y = (1f - ndc.Y) * 0.5f * screenHeight;

            return ndc.Z >= 0 && ndc.Z <= 1;
        }
    }
}
