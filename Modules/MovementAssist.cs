// neural v3.0
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Core;

namespace JaysAi.Finale.Modules
{
    public class MovementAssist
    {
        private readonly IInputSource _inputSource;
        private float _strafeBoostFactor = 1.15f;
        private bool _isActive;

        public MovementAssist(IInputSource inputSource)
        {
            _inputSource = inputSource;
        }

        public void Enable()
        {
            _isActive = true;
        }

        public void Disable()
        {
            _isActive = false;
        }

        public void Update(MovementState currentState)
        {
            if (!_isActive) return;

            // Auto-strafe logic: boost lateral movement to aid dodge or movement-shoot mechanics
            if (currentState.IsStrafingLeft)
            {
                _inputSource.InjectAnalogMovement(-_strafeBoostFactor, 0);
            }
            else if (currentState.IsStrafingRight)
            {
                _inputSource.InjectAnalogMovement(_strafeBoostFactor, 0);
            }

            // Future expansion could include adaptive sprint toggles or vault detection
        }

        public void SetStrafeBoostFactor(float boost)
        {
            _strafeBoostFactor = Math.Clamp(boost, 1.0f, 2.0f);
        }
    }

    public class MovementState
    {
        public bool IsStrafingLeft { get; set; }
        public bool IsStrafingRight { get; set; }

        // Expand later to include sprint, crouch, vault, etc.
    }
}
