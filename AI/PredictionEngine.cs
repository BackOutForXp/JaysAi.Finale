//monarch v2.1
using System;
using System.Collections.Generic;
using System.Linq;
using JaysAi.Finale.AI;
using SkiaSharp;

namespace JaysAi.AI
{
    public class PredictionEngine
    {
        private readonly ModelLoader modelLoader;
        private readonly ModelBridge modelBridge;
        private readonly AiMemory memory;

        public PredictionEngine(ModelLoader loader, ModelBridge bridge, AiMemory memory)
        {
            this.modelLoader = loader;
            this.modelBridge = bridge;
            this.memory = memory;
        }

        public void ProcessFrame(SKBitmap frame)
        {
            if (frame == null) return;

            var tensor = modelBridge.ConvertFrameToTensor(frame);
            using var results = modelLoader.RunInference(tensor);

            var predictions = modelBridge.ParseModelOutput(results);

            foreach (var prediction in predictions)
            {
                prediction.Timestamp = DateTime.UtcNow;
                memory.Add(prediction);
            }
        }

        public List<PredictionResult> GetLiveTargets()
        {
            return memory.GetRecent();
        }

        public PredictionResult? GetBestTarget()
        {
            return memory.GetClosestToCenter();
        }

        public void Reset()
        {
            memory.Clear();
        }
    }
}
