//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Aimbot
{
    public class TriggerSettings
    {
        public bool EnableTriggerbot { get; set; } = true;
        public float FireDistanceThreshold { get; set; } = 2.0f; // meters
        public float MinVisibilityRequired { get; set; } = 0.75f;
        public bool RequirePredictionLock { get; set; } = true;
        public int FireDelayMs { get; set; } = 30;

        public bool ShouldFire(TrackedTarget target)
        {
            if (!EnableTriggerbot || target == null || !target.IsAlive)
                return false;

            if (RequirePredictionLock && !target.IsPredictedHit)
                return false;

            if (target.VisibilityScore < MinVisibilityRequired)
                return false;

            if (target.Distance > FireDistanceThreshold)
                return false;

            return true;
        }
    }
}
