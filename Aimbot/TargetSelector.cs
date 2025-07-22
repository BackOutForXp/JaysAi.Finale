//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Modules;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.Aimbot
{
    public static class TargetSelector
    {
        public static TrackedTarget? GetFinalTarget(List<TrackedTarget> allTargets, AimSettings settings)
        {
            if (allTargets == null || allTargets.Count == 0)
                return null;

            var filtered = allTargets
                .Where(t => t.IsAlive && t.VisibilityScore > settings.MinVisibility && t.Distance < settings.MaxRange)
                .Where(t => t.AngleFromCrosshair <= settings.MaxFOV)
                .ToList();

            if (filtered.Count == 0)
                return null;

            return TargetPriority.SelectBestTarget(filtered, settings.TargetingMode);
        }
    }

    public class AimSettings
    {
        public float MaxFOV { get; set; } = 30f;
        public float MaxRange { get; set; } = 150f;
        public float MinVisibility { get; set; } = 0.3f;
        public TargetingMode TargetingMode { get; set; } = TargetingMode.Dynamic;
    }
}
