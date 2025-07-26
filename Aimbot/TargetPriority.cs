// neural v3.0
using System;
using System.Collections.Generic;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Aimbot
{
    public static class TargetPriority
    {
        public static TargetInfo GetHighestPriorityTarget(List<TargetInfo> targets, TargetingMode mode)
        {
            TargetInfo bestTarget = null;
            float bestScore = float.MinValue;

            foreach (var target in targets)
            {
                if (target == null || !target.IsValid || target.Distance > 300f)
                    continue;

                float score = mode switch
                {
                    TargetingMode.Closest => -target.Distance,
                    TargetingMode.LowestHealth => -target.Health,
                    TargetingMode.CenterFOV => -target.FovOffset,
                    TargetingMode.HighThreat => target.ThreatLevel,
                    _ => 0f
                };

                if (score > bestScore)
                {
                    bestScore = score;
                    bestTarget = target;
                }
            }

            return bestTarget;
        }
    }

    public enum TargetingMode
    {
        Closest,
        LowestHealth,
        CenterFOV,
        HighThreat
    }
}
