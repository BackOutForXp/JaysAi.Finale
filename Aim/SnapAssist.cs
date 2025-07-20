//monarch v2.1
using System;
using JaysAi.AI;
using JaysAi.Finale.Input;
using JaysAi.SystemLogic;

namespace JaysAi.Finale.Aim
{
    public class SnapAssist
    {
        private readonly StickAssist stickAssist;
        private readonly TargetSelector selector;
        private PredictionResult? currentTarget;

        public SnapAssist(StickAssist assist, TargetSelector targetSelector)
        {
            stickAssist = assist;
            selector = targetSelector;
        }

        public void UpdateTarget(float crosshairX, float crosshairY)
        {
            currentTarget = selector.FindNearestTarget(crosshairX, crosshairY);
            if (currentTarget != null)
            {
                float aimX = currentTarget.BoundingBox.MidX;
                float aimY = currentTarget.BoundingBox.MidY;
                stickAssist.UpdateTarget(aimX, aimY);
            }
        }

        public void ApplySnap(ref float outX, ref float outY, float currentX, float currentY)
        {
            if (currentTarget == null)
                return;

            stickAssist.ApplyAssist(ref outX, ref outY, currentX, currentY);
        }

        public void Clear()
        {
            currentTarget = null;
            stickAssist.Reset();
        }
    }
}
