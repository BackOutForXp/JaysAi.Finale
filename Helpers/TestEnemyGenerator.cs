// Neural v3.0 — TestEnemyGenerator.cs
using System;
using System.Collections.Generic;
using System.Windows;
using JaysAi.Finale.Visuals;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.Helpers
{
    public static class TestEnemyGenerator
    {
        private static readonly Random _rng = new();

        public static List<VisualEnemy> GenerateTestEnemies(int count, Size screenSize, TeamColor localTeam)
        {
            var enemies = new List<VisualEnemy>();

            for (int i = 0; i < count; i++)
            {
                var position = new Point(
                    _rng.NextDouble() * screenSize.Width,
                    _rng.NextDouble() * screenSize.Height
                );

                var enemy = new VisualEnemy
                {
                    ID = i,
                    ScreenPosition = position,
                    Health = _rng.Next(40, 101),
                    IsVisible = _rng.NextDouble() > 0.3,
                    Team = (TeamColor)_rng.Next(Enum.GetValues(typeof(TeamColor)).Length),
                    BoundingBox = new Rect(position.X - 25, position.Y - 50, 50, 100)
                };

                // Ensure it's not the same team as the local player (unless testing)
                if (enemy.Team != localTeam)
                {
                    enemies.Add(enemy);
                }
            }

            return enemies;
        }
    }
}
