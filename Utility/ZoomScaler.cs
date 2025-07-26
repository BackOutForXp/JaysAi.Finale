// neural v3.0
using System;

namespace JaysAi.Finale.Utility
{
    public static class ZoomScaler
    {
        /// <summary>
        /// Scales a sensitivity or aim multiplier based on zoom level (e.g., ADS or sniper scope).
        /// </summary>
        public static float ScaleWithZoom(float baseValue, float zoomLevel, float baseFov = 90f)
        {
            if (zoomLevel <= 0f)
                return baseValue;

            float adjustedFov = baseFov / zoomLevel;
            float scale = adjustedFov / baseFov;
            return baseValue * scale;
        }

        /// <summary>
        /// Normalizes zoom-adjusted coordinates back to native scale.
        /// </summary>
        public static float NormalizeZoomedInput(float zoomedValue, float zoomLevel)
        {
            return zoomLevel > 0f ? zoomedValue / zoomLevel : zoomedValue;
        }

        /// <summary>
        /// Returns a zoom factor to be applied to rendering or input.
        /// </summary>
        public static float GetZoomFactor(float currentFov, float defaultFov = 90f)
        {
            if (currentFov <= 0f)
                return 1f;

            return currentFov / defaultFov;
        }

        /// <summary>
        /// Inverse of zoom factor (for recoil/aim scaling in zoomed-in states).
        /// </summary>
        public static float GetInverseZoomFactor(float currentFov, float defaultFov = 90f)
        {
            float factor = GetZoomFactor(currentFov, defaultFov);
            return factor > 0f ? 1f / factor : 1f;
        }
    }
}
