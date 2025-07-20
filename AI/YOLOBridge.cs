//monarch v2.0
using System.Collections.Generic;
using JaysAi.Finale.src.Visuals;

namespace JaysAi.Finale.AI
{
    public class YOLOBridge
    {
        public float ConfidenceThreshold { get; set; } = 0.65f;
        public HashSet<int> TargetClassIds { get; set; } = new() { 0 }; // Typically: 0 = person

        public List<ESPModule.DetectedTarget> ParseDetections(List<YoloResult> yoloResults)
        {
            var validTargets = new List<ESPModule.DetectedTarget>();

            foreach (var result in yoloResults)
            {
                if (result.Confidence < ConfidenceThreshold)
                    continue;

                if (!TargetClassIds.Contains(result.ClassId))
                    continue;

                float centerX = result.X + result.Width / 2f;
                float centerY = result.Y + result.Height / 2f;

                validTargets.Add(new ESPModule.DetectedTarget
                {
                    ScreenX = centerX,
                    ScreenY = centerY,
                    Width = result.Width,
                    Height = result.Height,
                    Label = result.Label
                });
            }

            return validTargets;
        }
    }

    public class YoloResult
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float Confidence;
        public int ClassId;
        public string Label = "";
    }
}
