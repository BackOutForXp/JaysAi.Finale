//monarch v2.1
using JaysAi.Finale.Utility;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class AiMemory
    {
        private readonly List<PredictionResult> memory = new();
        private readonly TimeSpan memorySpan = TimeSpan.FromMilliseconds(500);

        public void Add(PredictionResult result)
        {
            memory.Add(result);
            CleanupOldEntries();
        }

        public List<PredictionResult> GetRecent()
        {
            CleanupOldEntries();
            return memory.ToList();
        }

        public PredictionResult? GetClosestToCenter()
        {
            CleanupOldEntries();
            return memory
                .OrderBy(p => Vector2.Distance(p.ScreenPosition, ScreenUtils.Center))
                .FirstOrDefault();
        }

        private void CleanupOldEntries()
        {
            var cutoff = DateTime.UtcNow - memorySpan;
            memory.RemoveAll(p => p.Timestamp < cutoff);
        }

        public void Clear()
        {
            memory.Clear();
        }
    }
}
