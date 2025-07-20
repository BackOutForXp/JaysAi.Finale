//monarch v2.1 – Target prioritization and visibility filter
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Aimbot
{
    public class TargetSelector
    {
        public float MaxTargetDistance { get; set; } = 1000f;
        public bool RequireVisible { get; set; } = true;
        public bool PrioritizeLowHealth { get; set; } = false;

        public DetectedTarget SelectBestTarget(Vector2 screenCenter, IEnumerable<DetectedTarget> candidates)
        {
            var filtered = candidates
                .Where(t => !RequireVisible || t.IsVisible)
                .Where(t => Vector2.Distance(screenCenter, t.ScreenPosition) <= MaxTargetDistance)
                .ToList();

            if (!filtered.Any())
                return null;

            if (PrioritizeLowHealth)
                return filtered.OrderBy(t => t.Health).First();

            return filtered
                .OrderBy(t => Vector2.Distance(screenCenter, t.ScreenPosition))
                .First();
        }

        public List<DetectedTarget> FilterTargets(Vector2 screenCenter, IEnumerable<DetectedTarget> all)
        {
            return all
                .Where(t => !RequireVisible || t.IsVisible)
                .Where(t => Vector2.Distance(screenCenter, t.ScreenPosition) <= MaxTargetDistance)
                .ToList();
        }
    }
}
