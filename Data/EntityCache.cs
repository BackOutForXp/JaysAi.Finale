// neural v3.0
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.Data
{
    public class EntityCache
    {
        private readonly ConcurrentDictionary<int, Enemy> _entities = new();

        public IReadOnlyCollection<Enemy> All => _entities.Values;

        public bool TryGet(int id, out Enemy enemy) => _entities.TryGetValue(id, out enemy);

        public void AddOrUpdate(Enemy enemy)
        {
            _entities.AddOrUpdate(enemy.Id, enemy, (_, _) => enemy);
        }

        public void Remove(int id)
        {
            _entities.TryRemove(id, out _);
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public List<Enemy> GetVisibleEnemies()
        {
            return _entities.Values.Where(e => e.IsVisible).ToList();
        }

        public List<Enemy> GetTrackedEnemies()
        {
            return _entities.Values.Where(e => e.IsTracked).ToList();
        }

        public List<Enemy> GetTargetedEnemies()
        {
            return _entities.Values.Where(e => e.IsTargeted).ToList();
        }

        public Enemy? GetClosestVisibleEnemy()
        {
            return _entities.Values
                .Where(e => e.IsVisible)
                .OrderBy(e => e.Distance)
                .FirstOrDefault();
        }

        public int Count => _entities.Count;
    }
}
