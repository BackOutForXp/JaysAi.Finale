//heavenly v3.0
using System.Collections.Generic;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Visuals;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public static class YoloDetector
    {
        private static IModelBridge _modelBridge;

        public static void Initialize(IModelBridge modelBridge)
        {
            _modelBridge = modelBridge;
            Logger.Log("YoloDetector initialized with model bridge.");
        }

        public static List<YoloBoundingBox> GetDetectedObjects()
        {
            if (_modelBridge == null || !_modelBridge.IsReady)
            {
                Logger.Warn("YoloDetector called before model bridge is ready.");
                return new List<YoloBoundingBox>();
            }

            var rawDetections = _modelBridge.Predict();
            var boundingBoxes = new List<YoloBoundingBox>();

            foreach (var detection in rawDetections)
            {
                if (IsValidDetection(detection))
                {
                    boundingBoxes.Add(new YoloBoundingBox
                    {
                        X = detection.X,
                        Y = detection.Y,
                        Width = detection.Width,
                        Height = detection.Height,
                        Confidence = detection.Confidence,
                        Label = detection.Label,
                        IsEnemy = detection.Label.ToLower() == "enemy"
                    });
                }
            }

            return boundingBoxes;
        }

        private static bool IsValidDetection(YoloBoundingBox box)
        {
            return box.Confidence > 0.5f && box.Width > 5 && box.Height > 5;
        }
    }
}
