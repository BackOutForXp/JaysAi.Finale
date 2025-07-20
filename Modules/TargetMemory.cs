//monarch v2.1
using System;
using System.Collections.Generic;
using JaysAi.AI;

namespace JaysAi.Modules
{
    public class TargetMemory
    {
        private PredictionResult? lastKnownTarget;
        private DateTime lastSeen;
        private readonly TimeSpan memoryTimeout = TimeSpan.FromMilliseconds(300);

        public void Update(PredictionResult? currentTarget)
        {
            if (currentTarget != null)
            {
                lastKnownTarget = currentTarget;
                lastSeen = DateTime.Now;
            }
        }

        public PredictionResult? GetValidTarget()
        {
            if (lastKnownTarget == null)
                return null;

            if ((DateTime.Now - lastSeen) <= memoryTimeout)
                return lastKnownTarget;

            return null;
        }

        public void Reset()
        {
            lastKnownTarget = null;
        }
    }
}
