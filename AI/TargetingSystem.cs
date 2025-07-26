// neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using JaysAi.Finale.Data;
using JaysAi.Finale.AI;
using JaysAi.Finale.Aim;

namespace JaysAi.Finale.AI
{
    public class TargetingSystem
    {
        private readonly List<TargetInfo> _targets = new();
        private readonly float _maxDistance = 150f;
        private readonly float _visibilityBonus = 1.5f;
        private readonly float _priorityDecay = 0.98f;

        public void UpdateTargets(List<Enemy> enemies, Vector3 playerPosition)
        {
            _targets.Clear();

            foreach (var enemy in enemies)
            {
                if (!enemy.IsAlive) continue;

                var distance = Vector3.Distance(enemy.Position, playerPosition);
                if (distance > _maxDistance) continue;

                var targetInfo = new TargetInfo(enemy, enemy.Position)
                {
                    Distance = distance,
                    IsVisible = enemy.IsVisible,
                    IsAlive = enemy.IsAlive,
                    AimWeightScore = CalculateScore(enemy, distance, enemy.IsVisible)
                };

                _targets.Add(targetInfo);
            }
        }

        private float CalculateScore(Enemy enemy, float distance, bool isVisible)
        {
            float score = 100f - distance;
            if (isVisible) score *= _visibilityBonus;

            score *= _priorityDecay;
            return Math.Clamp(score, 0f, 100f);
        }

        public TargetInfo GetBestTarget()
        {
            if (_targets.Count == 0)
                return null;

            return _targets
                .OrderByDescending(t => t.AimWeightScore)
                .ThenBy(t => t.Distance)
                .FirstOrDefault();
        }

        public List<TargetInfo> GetAllTargets() => _targets;
    }
}
