// Neural v3.1 — TrackTarget.cs
using System;
using System.Numerics;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    /// <summary>
    /// Manages the locked-on tracked target. 
    /// Sits between AimAssist and the AI’s real-time tracking loop.
    /// </summary>
    public class TrackTarget
    {
        public TrackedTarget Tracked { get; private set; }
        public bool IsLocked { get; private set; }

        private readonly TimeSpan visibilityTimeout = TimeSpan.FromMilliseconds(1000);

        public void LockOn(TrackedTarget target)
        {
            Tracked = target;
            IsLocked = true;
        }

        public void Unlock()
        {
            Tracked = null;
            IsLocked = false;
        }

        public void Update()
        {
            if (!IsLocked || Tracked == null)
                return;

            if (Tracked.IsLost(visibilityTimeout))
            {
                Unlock();
                return;
            }

            Tracked.Update(Tracked.Enemy.HeadPosition, Tracked.Enemy.IsVisible);
        }

        public Vector3 GetTargetPosition()
        {
            return Tracked?.PredictNextPosition() ?? Vector3.Zero;
        }

        public bool HasValidTarget()
        {
            return IsLocked && Tracked != null && !Tracked.IsLost(visibilityTimeout);
        }
    }
}
