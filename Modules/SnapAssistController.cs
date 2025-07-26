//neural v3.0

using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Structures;
using JaysAi.Finale.Core;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Modules
{
    public class SnapAssistController
    {
        private readonly TargetMemory _targetMemory;
        private readonly IInputSource _inputSource;
        private readonly SnapSettings _settings;

        public SnapAssistController(TargetMemory targetMemory, IInputSource inputSource, SnapSettings settings)
        {
            _targetMemory = targetMemory ?? throw new ArgumentNullException(nameof(targetMemory));
            _inputSource = inputSource ?? throw new ArgumentNullException(nameof(inputSource));
            _settings = settings ?? new SnapSettings();
        }

        public void Tick()
        {
            if (!_settings.Enabled)
                return;

            var target = _targetMemory.GetStrongestTarget(ScoreTarget);
            if (target == null)
                return;

            var aimOffset = CalculateSnapOffset(target.LastKnownObject);
            ApplySnap(aimOffset);
        }

        private float ScoreTarget(TrackedTarget target)
        {
            var dist = target.LastKnownObject.Distance;
            var vis = target.LastKnownObject.IsVisible ? 1f : 0f;
            return (_settings.WeightVisible * vis) + (_settings.WeightDistance / (dist + 0.01f));
        }

        private Vector2 CalculateSnapOffset(DetectedObject obj)
        {
            var currentCrosshair = _inputSource.GetCrosshairPosition();
            var targetPosition = obj.ScreenPosition;

            var offset = targetPosition - currentCrosshair;

            // Apply FOV check
            if (offset.Magnitude > _settings.MaxSnapFov)
                return Vector2.Zero;

            return offset * _settings.SnapStrength;
        }

        private void ApplySnap(Vector2 offset)
        {
            if (offset == Vector2.Zero)
                return;

            _inputSource.MoveMouse(offset);
        }
    }
}
