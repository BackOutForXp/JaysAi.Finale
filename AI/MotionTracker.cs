//monarch v2.1
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class MotionTracker
    {
        private readonly Dictionary<int, PredictionCache> entityHistory = new();

        public void Update(int entityId, FrameSnapshot snapshot)
        {
            if (!entityHistory.ContainsKey(entityId))
                entityHistory[entityId] = new PredictionCache();

            entityHistory[entityId].AddSnapshot(snapshot);
        }

        public (float dx, float dy)? PredictMovement(int entityId, int framesAhead = 1)
        {
            if (!entityHistory.TryGetValue(entityId, out var cache))
                return null;

            return cache.GetVelocityEstimate(framesAhead);
        }

        public void Clear()
        {
            entityHistory.Clear();
        }
    }
}
