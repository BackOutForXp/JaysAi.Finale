// File: Utility\MockEnemyGenerator.cs

using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Utility
{
    public static class MockEnemyGenerator
    {
        private static readonly Random _rng = new();

        public static List<Enemy> GenerateMockEnemies(int count = 5)
        {
            var list = new List<Enemy>();

            for (int i = 0; i < count; i++)
            {
                int id = i + 1;

                var enemy = new Enemy(id)
                {
                    Name = $"MockBot_{id}",
                    Health = _rng.Next(40, 100),
                    TeamId = 1,
                    IsVisible = true,
                    IsEnemy = true,

                    WorldPosition = new Vector3(
                        _rng.Next(0, 100),
                        0,
                        _rng.Next(20, 120)),

                    ScreenPosition = new Vector2(
                        _rng.Next(500, 1400),
                        _rng.Next(200, 800)),

                    Velocity = new Vector3(
                        _rng.Next(-2, 3),
                        0,
                        _rng.Next(-2, 3)),

                    ScreenVelocity = new Vector2(
                        _rng.Next(-5, 6),
                        _rng.Next(-5, 6))
                };

                list.Add(enemy);
            }

            return list;
        }
    }
}
