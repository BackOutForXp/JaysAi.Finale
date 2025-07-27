using System;
using System.Collections.Generic;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public class MemoryManager
    {
        private readonly Dictionary<int, TargetMemory> _memoryMap = new();
        private readonly TimeSpan _expirationTime = TimeSpan.FromSeconds(10);

        public TargetMemory GetOrCreate(int enemyId, string name = "Unknown")
        {
            if (_memoryMap.TryGetValue(enemyId, out var memory))
            {
                return memory;
            }

            memory = new TargetMemory(enemyId, name);
            _memoryMap[enemyId] = memory;
            return memory;
        }

        public void UpdateMemory(Enemy enemy, float score, bool snapSuccess)
        {
            var memory = GetOrCreate(enemy.Id, enemy.Name);
            memory.RecordObservation(enemy.Position, score, enemy.IsVisible);
            memory.RecordSnapAttempt(snapSuccess);
        }

        public IEnumerable<TargetMemory> GetAll()
        {
            return _memoryMap.Values;
        }

        public void PurgeExpired()
        {
            var now = DateTime.UtcNow;
            var expired = new List<int>();

            foreach (var pair in _memoryMap)
            {
                if ((now - pair.Value.LastSeen) > _expirationTime)
                {
                    expired.Add(pair.Key);
                }
            }

            foreach (var id in expired)
            {
                _memoryMap.Remove(id);
            }
        }

        public void Clear()
        {
            _memoryMap.Clear();
        }
    }
}
