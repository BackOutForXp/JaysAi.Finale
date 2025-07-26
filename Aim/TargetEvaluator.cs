// neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using JaysAi.Finale.Aim;

namespace JaysAi.Finale.Aim
{
    public class TargetEvaluator
    {
        private readonly float visibilityThreshold;
        private readonly float maxDistance;

        public TargetEvaluator(float visibilityThreshold = 0.5f, float maxDistance = 100f)
        {
            this.visibilityThreshold = visibilityThreshold;
            this.maxDistance = maxDistance;
        }

        public SnapTarget? EvaluateBestTarget(List<SnapTarget> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                return null;

            var filtered = candidates
                .Where(t => t.IsValid() && t.VisibilityScore >= visibilityThreshold && t.DistanceToCrosshair <= maxDistance)
                .ToList();

            if (filtered.Count == 0)
                return null;

            return filtered
                .OrderByDescending(t => t.GetPriorityScore())
                .FirstOrDefault();
        }

        public List<SnapTarget> SortByPriority(List<SnapTarget> targets)
        {
            return targets
                .Where(t => t.IsValid())
                .OrderByDescending(t => t.GetPriorityScore())
                .ToList();
        }
    }
}
