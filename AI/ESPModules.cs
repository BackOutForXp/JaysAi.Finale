//monarch v2.1
using System.Collections.Generic;
using JaysAi.AI;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.AI
{
    public class ESPModule
    {
        private readonly BoxRenderer boxRenderer;
        private readonly ESPStyleConfig style;

        public bool Enabled { get; set; } = true;

        public ESPModule(BoxRenderer renderer, ESPStyleConfig config)
        {
            boxRenderer = renderer;
            style = config;
        }

        public void Draw(List<PredictionResult> predictions)
        {
            if (!Enabled || predictions == null)
                return;

            foreach (var prediction in predictions)
            {
                var box = prediction.BoundingBox;
                boxRenderer.DrawBox(box, style);
            }
        }
    }
}
