// neural v3.0
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using JaysAi.Finale.AI;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Utility
{
    public class MockEnemy
    {
        public Vector3 Position { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public int Id { get; set; }
    }

    public class MockEnemyGenerator
    {
        private readonly List<MockEnemy> _enemies = new();
        private readonly Random _random = new();
        private readonly CancellationTokenSource _cancellation = new();
        private Task? _simulationTask;

        public IReadOnlyList<MockEnemy> Enemies => _enemies.AsReadOnly();

        public void StartGenerating(int count = 5, float radius = 15f)
        {
            _enemies.Clear();
            for (int i = 0; i < count; i++)
            {
                _enemies.Add(GenerateMock(i, radius));
            }

            _simulationTask = Task.Run(() => SimulateMovement(radius), _cancellation.Token);
        }

        public void StopGenerating()
        {
            _cancellation.Cancel();
            _simulationTask?.Wait();
        }

        private MockEnemy GenerateMock(int id, float radius)
        {
            return new MockEnemy
            {
                Id = id,
                Name = $"Bot_{id}",
                Position = GetRandomPosition(radius),
                Health = _random.Next(50, 101),
                IsVisible = _random.NextDouble() > 0.2
            };
        }

        private void SimulateMovement(float radius)
        {
            while (!_cancellation.Token.IsCancellationRequested)
            {
                for (int i = 0; i < _enemies.Count; i++)
                {
                    var enemy = _enemies[i];
                    enemy.Position += new Vector3(
                        (float)(_random.NextDouble() - 0.5),
                        0,
                        (float)(_random.NextDouble() - 0.5)
                    );

                    enemy.Position = ClampToRadius(enemy.Position, radius);
                    enemy.IsVisible = _random.NextDouble() > 0.15;
                    enemy.Health = Math.Clamp(enemy.Health + _random.Next(-2, 3), 0, 100);
                }

                Thread.Sleep(100);
            }
        }

        private Vector3 ClampToRadius(Vector3 pos, float radius)
        {
            if (pos.Length() > radius)
                return Vector3.Normalize(pos) * radius;
            return pos;
        }

        private Vector3 GetRandomPosition(float radius)
        {
            float angle = (float)(_random.NextDouble() * Math.PI * 2);
            float dist = (float)(_random.NextDouble() * radius);
            return new Vector3((float)Math.Cos(angle) * dist, 0, (float)Math.Sin(angle) * dist);
        }
    }
}
