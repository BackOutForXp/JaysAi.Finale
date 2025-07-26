using JaysAi.Finale.Data;

using JaysAi.Finale.SystemLogic;

=// neural v3.0
using System;
using System.Collections.Generic;
using System.Numerics;
using JaysAi.Finale.Data;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public class ProgressMemoryReader
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly Dictionary<int, TrackedTarget> _activeTargets;

        public ProgressMemoryReader(IEnemyProvider enemyProvider)
        {
            _enemyProvider = enemyProvider ?? throw new ArgumentNullException(nameof(enemyProvider));
            _activeTargets = new Dictionary<int, TrackedTarget>();
        }

        /// <summary>
        /// Updates internal memory snapshot by reading live target data and applying position smoothing.
        /// </summary>
        public void Refresh()
        {
            var enemies = _enemyProvider.GetEnemies();
            foreach (var enemy in enemies)
            {
                if (!_activeTargets.TryGetValue(enemy.ID, out var tracked))
                {
                    tracked = new TrackedTarget(enemy.ID);
                    _activeTargets[enemy.ID] = tracked;
                }

                UpdateTrackedTarget(tracked, enemy);
            }
        }

        /// <summary>
        /// Returns a read-only list of currently tracked enemies.
        /// </summary>
        public IReadOnlyCollection<TrackedTarget> GetTrackedTargets() => _activeTargets.Values;

        private void UpdateTrackedTarget(TrackedTarget tracked, Enemy enemy)
        {
            var deltaTime = TimeUtils.DeltaTime;
            var newVelocity = PredictionHelper.CalculateVelocity(tracked.Position, enemy.Position, deltaTime);

            tracked.LastPosition = tracked.Position;
            tracked.Position = enemy.Position;
            tracked.Velocity = newVelocity;
            tracked.LastSeenTime = DateTime.UtcNow;
            tracked.IsVisible = enemy.IsVisible;
            tracked.IsAlive = enemy.IsAlive;
        }

        /// <summary>
        /// Removes targets that haven't been updated in a while (stale).
        /// </summary>
        public void CleanupStaleTargets(double staleSeconds = 1.5)
        {
            var cutoff = DateTime.UtcNow - TimeSpan.FromSeconds(staleSeconds);
            var staleKeys = new List<int>();

            foreach (var kvp in _activeTargets)
            {
                if (kvp.Value.LastSeenTime < cutoff)
                    staleKeys.Add(kvp.Key);
            }

            foreach (var key in staleKeys)
                _activeTargets.Remove(key);
        }
    }
}
