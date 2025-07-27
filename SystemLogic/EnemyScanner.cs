// Neural v3.1
using JaysAi.Finale.Data;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.SystemLogic
{
    public class EnemyScanner
    {
        private readonly Random _rand = new();

        // TODO: Replace mock with memory or visual detection system
        public List<TrackedTarget> Scan()
        {
            var list = new List<TrackedTarget>();

            // Simulated target
            var simulated = new TrackedTarget
            {
                Id = 1,
                Name = "SimTarget",
                Position = new Vector3(_rand.Next(10, 100), 0, _rand.Next(10, 100)),
                Velocity = new Vector3(_rand.Next(-2, 2), 0, _rand.Next(-2, 2)),
                Head = new Vector3(0, 1.6f, 0),
                Chest = new Vector3(0, 1.2f, 0),
                Health = 100,
                IsVisible = true,
                ScreenBox = new RectangleF(200, 200, 80, 160),
                ScreenHead = new Vector2(240, 200),
                PredictedScreenHead = new Vector2(245, 198)
            };

            list.Add(simulated);

            return list;
        }
    }
}
