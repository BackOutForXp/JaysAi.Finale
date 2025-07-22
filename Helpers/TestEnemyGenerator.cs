//heavenly v3.0 – Debug Enemy Spawner
using System;
using System.Collections.Generic;
using System.Windows;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.Helpers
{
    public static class TestEnemyGenerator
    {
        private static readonly Random Random = new();

        public static List<Enemy> GenerateMockEnemies(int count = 5)
        {
            var enemies = new List<Enemy>();

            for (int i = 0; i < count; i++)
            {
                enemies.Add(new Enemy
                {
                    Id = Guid.NewGuid().ToString(),
                    Position = new Point(Random.Next(100, 1920), Random.Next(100, 1080)),
                    Health = Random.Next(50, 100),
                    IsVisible = Random.NextDouble() > 0.3,
                    Distance = Random.NextDouble() * 100
                });
            }

            return enemies;
        }
    }
}
