// neural v3.0
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class PredictionCache
    {
        private readonly Dictionary<int, CachedPrediction> _cache = new();

        public void Store(int entityId, Vector3 predictedPosition, float timestamp)
        {
            if (_cache.TryGetValue(entityId, out var existing))
            {
                existing.PredictedPosition = predictedPosition;
                existing.Timestamp = timestamp;
            }
            else
            {
                _cache[entityId] = new CachedPrediction
                {
                    EntityId = entityId,
                    PredictedPosition = predictedPosition,
                    Timestamp = timestamp
                };
            }
        }

        public Vector3? Get(int entityId, float currentTime, float maxAge = 0.2f)
        {
            if (_cache.TryGetValue(entityId, out var cached))
            {
                if ((currentTime - cached.Timestamp) <= maxAge)
                    return cached.PredictedPosition;
            }
            return null;
        }

        public void ClearOld(float currentTime, float maxAge = 0.5f)
        {
            List<int> expiredKeys = new();

            foreach (var kvp in _cache)
            {
                if ((currentTime - kvp.Value.Timestamp) > maxAge)
                    expiredKeys.Add(kvp.Key);
            }

            foreach (var key in expiredKeys)
                _cache.Remove(key);
        }

        public void ClearAll() => _cache.Clear();

        private class CachedPrediction
        {
            public int EntityId;
            public Vector3 PredictedPosition;
            public float Timestamp;
        }
    }
}
