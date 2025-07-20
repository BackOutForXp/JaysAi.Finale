//monarch v2.1
using System;
using System.Collections.Generic;
using JaysAi.AI.Models;

namespace JaysAi.Finale.AI
{
    public class TargetTracker
    {
        private List<TargetData> targets = new();
        private TargetData currentTarget;

        public void UpdateTargets(List<TargetData> newDetections)
        {
            targets = newDetections;
            currentTarget = SelectBestTarget(targets);
        }

        public TargetData GetCurrentTarget() => currentTarget;

        private TargetData SelectBestTarget(List<TargetData> candidates)
        {
            TargetData best = null;
            float closestDistance = float.MaxValue;

            foreach (var target in candidates)
            {
                float dist = DistanceFromCrosshair(target);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    best = target;
                }
            }

            return best;
        }

        private float DistanceFromCrosshair(TargetData target)
        {
            float centerX = 0.5f; // normalized center
            float centerY = 0.5f;

            float dx = target.CenterX - centerX;
            float dy = target.CenterY - centerY;

            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public List<TargetData> GetAllTargets() => targets;
        public bool HasTargets() => targets != null && targets.Count > 0;
    }
}
