//monarch v2.1
using System;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Aimbot
{
    public class EntityLock
    {
        public FrameSnapshot? CurrentTarget { get; private set; }
        private int framesSinceSeen = 0;
        private readonly int maxLostFrames;

        public EntityLock(int maxLostFrames = 5)
        {
            this.maxLostFrames = maxLostFrames;
        }

        public void UpdateLock(FrameSnapshot? newTarget)
        {
            if (newTarget == null)
            {
                framesSinceSeen++;
                if (framesSinceSeen > maxLostFrames)
                {
                    CurrentTarget = null;
                    framesSinceSeen = 0;
                }
                return;
            }

            if (CurrentTarget == null || newTarget.ID != CurrentTarget.ID)
            {
                CurrentTarget = newTarget;
                framesSinceSeen = 0;
            }
            else
            {
                CurrentTarget = newTarget;
                framesSinceSeen = 0;
            }
        }

        public bool HasValidLock()
        {
            return CurrentTarget != null;
        }
    }
}
