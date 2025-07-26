// neural v3.0
using System;
using System.Numerics;

namespace JaysAi.Finale.Modules.Aim
{
    public class AimSmoother
    {
        private float _smoothingFactor;
        private Vector2 _previousAim;

        public AimSmoother(float initialSmoothing = 0.2f)
        {
            _smoothingFactor = Math.Clamp(initialSmoothing, 0.01f, 1f);
            _previousAim = Vector2.Zero;
        }

        public void SetSmoothingFactor(float factor)
        {
            _smoothingFactor = Math.Clamp(factor, 0.01f, 1f);
        }

        public Vector2 SmoothAim(Vector2 currentAim, Vector2 targetAim)
        {
            var smoothedX = Lerp(currentAim.X, targetAim.X, _smoothingFactor);
            var smoothedY = Lerp(currentAim.Y, targetAim.Y, _smoothingFactor);

            _previousAim = new Vector2(smoothedX, smoothedY);
            return _previousAim;
        }

        public Vector2 GetPreviousAim() => _previousAim;

        private float Lerp(float start, float end, float t)
        {
            return start + (end - start) * t;
        }
    }
}
