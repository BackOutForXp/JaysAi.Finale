// neural v3.0
using JaysAi.Finale.Data;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class AimPathPredicator
    {
        private readonly Dictionary<int, Vector2> _velocityCache = new();
        private readonly float _smoothingFactor = 0.15f;

        public Vector2 PredictNextPosition(int targetId, Vector2 currentPosition)
        {
            if (_velocityCache.TryGetValue(targetId, out var previousVelocity))
            {
                var predicted = currentPosition + previousVelocity;
                return Vector2.Lerp(currentPosition, predicted, _smoothingFactor);
            }

            return currentPosition;
        }

        public void UpdateVelocity(int targetId, Vector2 oldPosition, Vector2 newPosition)
        {
            var velocity = newPosition - oldPosition;
            _velocityCache[targetId] = velocity;
        }

        public void Clear()
        {
            _velocityCache.Clear();
        }

        public bool HasVelocity(int targetId) => _velocityCache.ContainsKey(targetId);

        public Vector2? GetVelocity(int targetId)
        {
            return _velocityCache.TryGetValue(targetId, out var velocity)
                ? velocity
                : null;
        }
    }
}
