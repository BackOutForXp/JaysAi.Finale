//monarch v2.1 – YOLO ↔ ESP Target Bridge
using System.Collections.Generic;
using JaysAi.Finale.Visuals;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public static class YOLOBridge
    {
        public static List<TrackedTarget> ParseDetections(List<YoloBoundingBox> rawDetections)
        {
            var targets = new List<TrackedTarget>();

            foreach (var box in rawDetections)
            {
                if (!IsEnemyClass(box.Label)) continue;

                var target = new TrackedTarget
                {
                    Id = box.Id,
                    X = box.X + (box.Width / 2f),
                    Y = box.Y + (box.Height / 2f),
                    Width = box.Width,
                    Height = box.Height,
                    IsVisible = true,
                    IsEnemy = true,
                    VelocityX = 0f, // To be filled in PredictionEngine
                    VelocityY = 0f
                };

                targets.Add(target);
                Logger.Log($"[YOLOBridge] Target found: {box.Label} at ({target.X}, {target.Y})", LogLevel.Debug);
            }

            return targets;
        }

        private static bool IsEnemyClass(string label)
        {
            // Can be customized per game model
            return label.ToLower().Contains("enemy") || label.ToLower() == "person";
        }
    }
}
