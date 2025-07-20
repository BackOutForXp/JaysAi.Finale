// File: AI/TargetingSystem.cs
using JaysAi.Finale.Data;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public static class TargetingSystem
    {
        private static readonly List<Enemy> _enemies = new();
        private static readonly Random _random = new();

        public static IReadOnlyList<Enemy> GetEnemies() => _enemies;

        /// <summary>
        /// Updates enemies with mock data. Replace with memory scan or capture integration later.
        /// </summary>
        public static void UpdateTargets()
        {
            _enemies.Clear();

            for (int i = 0; i < 5; i++)
            {
                var enemy = new Enemy
                {
                    Name = $"Bot_{i + 1}",
                    WorldPosition = new Vector3(
                        _random.Next(0, 100),
                        0,
                        _random.Next(20, 120)),

                    ScreenPosition = new Vector2(
                        _random.Next(400, 1400),
                        _random.Next(200, 800)),

                    Velocity = new Vector3(
                        _random.Next(-2, 2),
                        0,
                        _random.Next(-2, 2)),

                    ScreenVelocity = new Vector2(
