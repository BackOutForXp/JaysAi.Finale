using System;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Utility
{
    public class ZoomController
    {
        private readonly AppSettings _settings;

        private float _currentZoomFactor = 1.0f;
        private float _targetZoomFactor = 1.0f;
        private float _smoothing = 0.1f;

        public ZoomController(AppSettings settings)
        {
            _settings = settings;
        }

        public float CurrentZoom => _currentZoomFactor;

        public void UpdateZoom(bool isAiming)
        {
            _targetZoomFactor = isAiming
                ? _settings.ADS_ZoomMultiplier
                : 1.0f;

            // Smoothly interpolate zoom factor
            _currentZoomFactor = Lerp(_currentZoomFactor, _targetZoomFactor, _smoothing);
        }

        public float ApplyZoomToValue(float baseValue)
        {
            return baseValue * _currentZoomFactor;
        }

        public void SetSmoothing(float smoothing)
        {
            _smoothing = Math.Clamp(smoothing, 0.01f, 1.0f);
        }

        private float Lerp(float from, float to, float by)
        {
            return from + (to - from) * by;
        }
    }
}
