//monarch v2.0
using System;

namespace JaysAi.Finale.Input
{
    /// <summary>
    /// Handles smooth, relative mouse movement based on AI or controller input.
    /// This is used to emulate natural aim shifts without snapping.
    /// </summary>
    public class MouseEmulator
    {
        public float SmoothingFactor { get; set; } = 0.25f;
        public float AimSpeedMultiplier { get; set; } = 1.0f;

        private float _accumulatedX;
        private float _accumulatedY;

        /// <summary>
        /// Applies an aim delta to be smoothed and injected as real mouse movement.
        /// </summary>
        /// <param name="deltaX">Raw X aim adjustment.</param>
        /// <param name="deltaY">Raw Y aim adjustment.</param>
        public void ApplyAim(float deltaX, float deltaY)
        {
            _accumulatedX += deltaX * AimSpeedMultiplier;
            _accumulatedY += deltaY * AimSpeedMultiplier;

            var smoothedX = _accumulatedX * SmoothingFactor;
            var smoothedY = _accumulatedY * SmoothingFactor;

            if (Math.Abs(smoothedX) >= 1 || Math.Abs(smoothedY) >= 1)
            {
                int moveX = (int)Math.Round(smoothedX);
                int moveY = (int)Math.Round(smoothedY);

                InputInjector.MoveMouseRelative(moveX, moveY);

                _accumulatedX -= moveX;
                _accumulatedY -= moveY;
            }
        }

        /// <summary>
        /// Resets the internal aim accumulators.
        /// </summary>
        public void Reset()
        {
            _accumulatedX = 0;
            _accumulatedY = 0;
        }
    }
}
