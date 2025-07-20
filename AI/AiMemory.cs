//monarch v2.1 – Visual Detection & Memory Recall Engine
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class AiMemory
    {
        private readonly Dictionary<int, Vector2> _entityPositionMemory;
        private readonly Dictionary<int, DateTime> _lastSeenTime;

        public AiMemory()
        {
            _entityPositionMemory = new Dictionary<int, Vector2>();
            _lastSeenTime = new Dictionary<int, DateTime>();
        }

        public void UpdateMemory(int entityId, Vector2 position)
        {
            _entityPositionMemory[entityId] = position;
            _lastSeenTime[entityId] = DateTime.UtcNow;
        }

        public Vector2? GetLastKnownPosition(int entityId)
        {
            if (_entityPositionMemory.ContainsKey(entityId))
                return _entityPositionMemory[entityId];

            return null;
        }

        public bool WasRecentlySeen(int entityId, double seconds = 1.5)
        {
            if (_lastSeenTime.TryGetValue(entityId, out var seenTime))
            {
                return (DateTime.UtcNow - seenTime).TotalSeconds <= seconds;
            }
            return false;
        }

        public void ClearOldMemory(double expirationTimeInSeconds = 5.0)
        {
            var now = DateTime.UtcNow;
            var toRemove = new List<int>();

            foreach (var kvp in _lastSeenTime)
            {
                if ((now - kvp.Value).TotalSeconds > expirationTimeInSeconds)
                    toRemove.Add(kvp.Key);
            }

            foreach (var id in toRemove)
            {
                _entityPositionMemory.Remove(id);
                _lastSeenTime.Remove(id);
            }
        }
    }
}
