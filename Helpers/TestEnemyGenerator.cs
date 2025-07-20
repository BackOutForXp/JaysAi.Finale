using System;
using System.Collections.Generic;
using System.Windows;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.Helpers
{
    public static class TestEnemyGenerator
    {
        private static readonly Random _rand = new();

        public static List<Enemy> GenerateTestEnemies(int count = 5)
        {
            var enemies = new List<Enemy>();

            for (int i = 0; i < count; i++)
            {
                var pos = new Point(
                    _rand.Next(200, (int)SystemParameters.PrimaryScreenWidth - 200),
                    _rand.Next(200, (int)SystemParameters.PrimaryScreenHeight - 200)
                );

                var vel = new Vector(
                    _rand.NextDouble() * 2 - 1,
                    _rand.NextDouble() * 2 - 1
                );

                enemies.Add(new Enemy
                {
                    Name = $"Bot{i + 1}",
                    Position = pos,
                    Velocity = vel,
                    IsVisible = true,
                    Health = _rand.Next(30, 100),
                    HeadPosition = new Point(pos.X, pos.Y - 50),
                    ChestPosition = new Point(pos.X, pos.Y - 30),
                    LastSeen = DateTime.UtcNow
                });
            }

            return enemies;
        }
    }
}
