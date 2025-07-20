// File: AI/DummyEnemyProvider.cs
using JaysAi.Finale.Data;
using SkiaSharp;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class DummyEnemyProvider : IEnemyProvider
    {
        private readonly List<Enemy> _enemies;

        public DummyEnemyProvider()
        {
            _enemies = new List<Enemy>
            {
                new Enemy
                {
                    Name = "Enemy_01",
                    IsAlive = true,
                    ScreenBounds = new SKRect(500, 300, 560, 360)
                },
                new Enemy
                {
                    Name = "Enemy_02",
                    IsAlive = true,
                    ScreenBounds = new SKRect(800, 450, 860, 510)
                }
            };
        }

        public List<Enemy> GetVisibleEnemies()
        {
            return _enemies;
        }
    }
}
