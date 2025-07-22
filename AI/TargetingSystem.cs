//heavenly v3.0
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public static class TargetingSystem
    {
        public static TargetInfo CurrentTarget { get; private set; }
        public static List<TargetInfo> VisibleTargets { get; private set; } = new();

        public static void UpdateTargets(IEnumerable<TargetInfo> detectedTargets)
        {
            // Filter only visible and enemy targets
            VisibleTargets = detectedTargets
                .Where(t => t.IsVisible && t.IsEnemy)
                .OrderBy(t => t.Distance)
                .ToList();

            CurrentTarget = SelectHighestPriorityTarget(VisibleTargets);
        }

        private static TargetInfo SelectHighestPriorityTarget(List<TargetInfo> targets)
        {
            if (targets.Count == 0) return null;

            // Simple heuristic: closest + highest threat
            return targets
                .OrderByDescending(t => t.ThreatLevel)
                .ThenBy(t => t.Distance)
                .FirstOrDefault();
        }

        public static void Reset()
        {
            CurrentTarget = null;
            VisibleTargets.Clear();
        }

        public static bool HasLock => CurrentTarget != null;
    }
}
