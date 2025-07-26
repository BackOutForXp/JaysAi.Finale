// neural v3.0
using JaysAi.Finale.Data;
using JaysAi.Finale.Helpers;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class DummyEnemyProvider : IEnemyProvider
    {
        private readonly Random _rand = new();

        public List<Enemy> GetVisibleEnemies()
        {
            var enemies = new List<Enemy>();

            for (int i = 0; i < 3; i++) // Simulate 3 dummies
            {
                var position = new Vector3(
                    _rand.Next(-50, 50),
                    _rand.Next(-50, 50),
                    _rand.Next(0, 10)
                );

                var headPos = position + new Vector3(0, 0, 1.7f); // fake head height
                var bones = new Dictionary<BoneType, Vector3>
                {
                    { BoneType.Head, headPos },
                    { BoneType.Chest, position + new Vector3(0, 0, 1.2f) },
                    { BoneType.Pelvis, position + new Vector3(0, 0, 0.9f) }
                };

                enemies.Add(new Enemy
                {
                    ID = i,
                    IsVisible = true,
                    Position = position,
                    Health = 100,
                    Bones = bones,
                    Team = 2,
                    Velocity = new Vector3(_rand.Next(-1, 2), _rand.Next(-1, 2), 0),
                    LastSeen = DateTime.UtcNow
                });
            }

            return enemies;
        }
    }
}
