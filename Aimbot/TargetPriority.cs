//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.Aimbot
{
    public static class TargetPriority
    {
        public static TrackedTarget? SelectBestTarget(List<TrackedTarget> candidates, TargetingMode mode)
        {
            if (candidates == null || candidates.Count == 0)
                return null;

            switch (mode)
            {
                case TargetingMode.Closest:
                    return candidates.OrderBy(t => t.Distance).FirstOrDefault();

                case TargetingMode.CenterFOV:
                    return candidates.OrderBy(t => t.AngleFromCrosshair).FirstOrDefault();

                case TargetingMode.LeastMovement:
                    return candidates.OrderBy(t => t.Velocity.Magnitude()).FirstOrDefault();

                case TargetingMode.MostVisible:
                    return candidates.OrderByDescending(t => t.VisibilityScore).FirstOrDefault();

                case TargetingMode.Dynamic:
                    return candidates
                        .OrderBy(t =>
                            t.AngleFromCrosshair * 0.5f +
                            t.Distance * 0.3f -
                            t.VisibilityScore * 0.2f)
                        .FirstOrDefault();

                default:
                    return candidates.OrderBy(t => t.Distance).FirstOrDefault();
            }
        }
    }

    public enum TargetingMode
    {
        Closest,
        CenterFOV,
        LeastMovement,
        MostVisible,
        Dynamic
    }
}
