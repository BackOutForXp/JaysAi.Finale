//monarch v2.0
using System;

namespace JaysAi.Finale.AI
{
    /// <summary>
    /// Handles analog stick-style aim assist for use with controller-like behavior.
    /// Integrates with prediction logic and input state to simulate natural stick control.
    /// </summary>
    public class StickXModule
    {
        public bool Enabled { get; set; } = false;
        public float StickSpeed { get; set; } = 1.5f;
        public float AimFriction { get; set; } = 0.25f;
        public float DeadZone { get; set; } = 0.05f;

        private float _currentX;
        private float _currentY;

        /// <summary>
        /// Updates internal aim values toward the desired target direction.
        /// </summary>
        /// <param name="targetX">Target X aim delta.</param>
        /// <param name="targetY">Target Y aim delta.</param>
        public void UpdateAim(float targetX, float targetY)
        {
            if (!Enabled)
                return;

            // Apply friction and smoothing
            _currentX += (targetX - _currentX) * StickSpeed * (1f - AimFriction);
            _currentY += (targetY - _currentY) * StickSpeed * (1f - AimFriction);

            // Clamp deadzone
            if (Math.Abs(_currentX) < DeadZone) _currentX = 0;
            if (Math.Abs(_currentY) < DeadZone) _currentY = 0;
        }

        /// <summary>
        /// Gets the current stick output to apply to aim assist.
        /// </summary>
        public (float X, float Y) GetStickOutput()
        {
            return (_currentX, _currentY);
        }

        /// <summary>
        /// Resets internal aim state.
        /// </summary>
        public void Reset()
        {
            _currentX = 0;
            _currentY = 0;
        }
    }
}
