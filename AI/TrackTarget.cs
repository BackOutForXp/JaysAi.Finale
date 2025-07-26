// neural v3.0
using System;
using JaysAi.Finale.Data;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
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

            // Maintain smoothing update
            Tracked.Update(Tracked.Enemy.Position, Tracked.Enemy.IsVisible);
        }

        public Vector3 GetTargetPosition()
        {
            if (Tracked == null)
                return Vector3.Zero;

            return Tracked.PredictNextPosition();
        }

        public bool HasValidTarget()
        {
            return IsLocked && Tracked != null && !Tracked.IsLost(visibilityTimeout);
        }
    }
}
