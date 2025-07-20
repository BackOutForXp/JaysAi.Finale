// File: Modules/EnemyScanner.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace JaysAi.Finale.Modules
{
    public class EnemyScanner
    {
        private readonly AppSettings _settings;

        public EnemyScanner(AppSettings settings)
        {
            _settings = settings;
        }

        public List<Enemy> GetVisibleEnemies(Vector2 playerPosition)
        {
            var enemies = TargetingSystem.GetEnemies();

            return enemies
                .Where(e =>
                    e.IsEnemy &&
                    e.IsVisible &&
                    e.Health > 0 &&
                    Vector2.Distance(playerPosition, e.ScreenPosition) <= _settings.MaxScanDistance)
                .ToList();
        }
    }
}
