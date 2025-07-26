// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Input;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using System;
using System.Numerics;

namespace JaysAi.Finale.Modules
{
    public class SilentAim
    {
        private readonly IInputSource _inputSource;
        private readonly IAimTargetProvider _targetProvider;

        public bool Enabled { get; set; } = false;
        public float ActivationFOV { get; set; } = 5f;
        public bool RequireADS { get; set; } = true;

        public SilentAim(IInputSource inputSource, IAimTargetProvider targetProvider)
        {
            _inputSource = inputSource;
            _targetProvider = targetProvider;
        }

        public void Update(PlayerState playerState)
        {
            if (!Enabled || (RequireADS && !playerState.IsAiming))
                return;

            var closestTarget = _targetProvider.GetClosestTargetWithinFOV(ActivationFOV);
            if (closestTarget == null || !closestTarget.IsVisible)
                return;

            Vector2 adjustedAim = CalculateSilentAngle(playerState.ViewAngles, closestTarget.Position);
            ApplySilentAim(adjustedAim);
        }

        private Vector2 CalculateSilentAngle(Vector2 currentView, Vector3 targetPos)
        {
            Vector2 desiredAngle = AngleMath.CalculateAngleFromPosition(currentView, targetPos);
            return AngleMath.ClampAngle(desiredAngle);
        }

        private void ApplySilentAim(Vector2 newViewAngle)
        {
            _inputSource.OverrideViewAngle(newViewAngle); // Silent aim injection
        }
    }
}
