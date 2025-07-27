// Neural v3.1 — VisibilityFilter.cs
using JaysAi.Finale.Data;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.AI
{
    public static class VisibilityFilter
    {
        public static List<TrackedTarget> FilterVisible(List<TrackedTarget> targets)
        {
            if (targets == null || targets.Count == 0)
                return new List<TrackedTarget>();

            return targets.Where(t => t.IsVisible && t.ScreenBox.HasValue).ToList();
        }

        public static TrackedTarget GetFirstVisible(List<TrackedTarget> targets)
        {
            return targets?.FirstOrDefault(t => t.IsVisible && t.ScreenBox.HasValue);
        }

        public static bool HasVisibleTargets(List<TrackedTarget> targets)
        {
            return targets?.Any(t => t.IsVisible && t.ScreenBox.HasValue) ?? false;
        }
    }
}
