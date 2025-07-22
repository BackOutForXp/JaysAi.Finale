//heavenly v3.0.0 – Adaptive Behavior Trigger Logic
using JaysAi.Finale.Modules;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.SystemLogic;
using System;

namespace JaysAi.Finale.AI
{
    public static class BehaviorTrigger
    {
        public static bool ShouldActivateSnap(DetectedObject obj)
        {
            return obj.IsEnemy && obj.Distance < 750f && obj.VisibilityScore > 0.7f;
        }

        public static bool ShouldEnableSilentAim(TargetInfo info)
        {
            return info.IsMoving && info.Speed > 1.2f && info.IsInsideFov;
        }

        public static bool ShouldFire(TargetInfo info)
        {
            return info.IsEnemy && info.IsVisible && info.TimeSinceSpotted < 0.25f;
        }

        public static bool IsThreatLevelHigh(TargetInfo info)
        {
            return info.IsEnemy && info.Aggression > 0.8f && info.Distance < 500f;
        }

        public static string GetReactionLabel(TargetInfo info)
        {
            if (IsThreatLevelHigh(info))
                return "HIGH THREAT";
            if (info.IsMoving)
                return "TRACKING";
            return "PASSIVE";
        }
    }
}
