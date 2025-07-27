// Neural v3.1
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.Helpers;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class MotionTracker
    {
        private readonly Dictionary<Guid, TrackedTarget> _previousState = new();

        public void Initialize()
        {
            _previousState.Clear();
        }

        public void ProcessMovementData(List<TrackedTarget>? currentTargets = null)
        {
            if (currentTargets == null) return;

            foreach (var target in currentTargets)
            {
                if (!target.IsValid) continue;

                if (_previousState.TryGetValue(target.Id, out var prev))
                {
                    var velocity = PredictionHelper.EstimateVelocity(prev.WorldPosition, target.WorldPosition, TimeUtils.DeltaTime);
                    target.Velocity = velocity;
                    target.MovementSpeed = velocity.Length();

                    target.IsSprinting = target.MovementSpeed > 5f;
                    target.IsStrafing = Math.Abs(velocity.X) > Math.Abs(velocity.Z);
                }
                else
                {
                    target.Velocity = Vector3.Zero;
                    target.MovementSpeed = 0f;
                }

                // Store for next frame
                target.PreviousWorldPosition = target.WorldPosition;
                _previousState[target.Id] = target;
            }
        }
    }
}
