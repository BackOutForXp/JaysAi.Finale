// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
using System.Collections.Generic;

namespace JaysAi.Finale.Aimbot
{
    public class TargetSelector
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly TargetingMode _mode;

        public TargetSelector(IEnemyProvider enemyProvider, TargetingMode mode = TargetingMode.Closest)
        {
            _enemyProvider = enemyProvider;
            _mode = mode;
        }

        public TargetInfo SelectTarget()
        {
            var allEnemies = _enemyProvider.GetEnemies();
            var validTargets = new List<TargetInfo>();

            foreach (var enemy in allEnemies)
            {
                if (enemy == null || !enemy.IsVisible || enemy.Distance > 300f)
                    continue;

                var target = new TargetInfo
                {
                    EntityId = enemy.Id,
                    Position = enemy.Position,
                    Distance = enemy.Distance,
                    Health = enemy.Health,
                    FovOffset = CalculateFovOffset(enemy),
                    ThreatLevel = EvaluateThreat(enemy)
                };

                validTargets.Add(target);
            }

            return TargetPriority.GetHighestPriorityTarget(validTargets, _mode);
        }

        private float CalculateFovOffset(Enemy enemy)
        {
            // Plug in FOV distance logic based on aim direction vs target
            return VectorMathHelper.CalculateFovOffset(enemy.Position);
        }

        private float EvaluateThreat(Enemy enemy)
        {
            // Placeholder logic for threat evaluation (can evolve later)
            return 1.0f / (enemy.Distance + 1.0f);
        }
    }
}
