// neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class YoloDetector
    {
        private readonly YOLOBridge yoloBridge;
        private readonly float confidenceThreshold;
        private readonly string[] targetLabels;

        public YoloDetector(YOLOBridge bridge, float threshold = 0.45f, string[] validLabels = null)
        {
            yoloBridge = bridge;
            confidenceThreshold = threshold;
            targetLabels = validLabels ?? new[] { "enemy", "person", "target" };
        }

        public List<YoloBoundingBox> Detect(Mat frame)
        {
            var rawBoxes = yoloBridge.RunDetection(frame);
            return FilterDetections(rawBoxes);
        }

        private List<YoloBoundingBox> FilterDetections(IEnumerable<YoloBoundingBox> detections)
        {
            return detections
                .Where(box =>
                    box.Confidence >= confidenceThreshold &&
                    targetLabels.Any(label => string.Equals(box.Label, label, StringComparison.OrdinalIgnoreCase))
                )
                .OrderByDescending(b => b.Confidence)
                .ToList();
        }
    }
}
