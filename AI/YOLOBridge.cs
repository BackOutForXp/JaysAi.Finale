//heavenly v3.0
using System;
using System.Collections.Generic;
using OpenCvSharp;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.AI
{
    public static class YOLOBridge
    {
        private static YOLODetector _detector;

        public static void Initialize(string modelPath)
        {
            if (_detector == null)
            {
                _detector = new YOLODetector(modelPath);
            }
        }

        public static List<YoloBoundingBox> DetectEnemies(Mat frame)
        {
            var results = _detector.Detect(frame);
            var boxes = new List<YoloBoundingBox>();
            int idCounter = 0;

            foreach (var result in results)
            {
                var box = new YoloBoundingBox(idCounter++, result.BoundingBox, result.Label, result.Confidence);

                // Classification rules (can be upgraded with user config)
                box.Classify(
                    label => label.ToLower().Contains("enemy"),
                    label => label.ToLower().Contains("teammate") || label.ToLower().Contains("ally")
                );

                boxes.Add(box);
            }

            return boxes;
        }

        public static void DebugOverlay(Mat frame)
        {
            var detections = DetectEnemies(frame);

            foreach (var box in detections)
            {
                if (box.IsEnemy)
                {
                    AiOverlay.QueueRectangle(
                        box.BoundingBox.X,
                        box.BoundingBox.Y,
                        box.BoundingBox.Width,
                        box.BoundingBox.Height,
                        box.Label,
                        OverlayColor.Red
                    );
                }
            }
        }
    }
}
