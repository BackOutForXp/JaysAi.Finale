//heavenly v3.0.0 – Model Inference Routing Layer
using System.Collections.Generic;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.AI
{
    public static class ModelBridge
    {
        /// <summary>
        /// Passes a video frame to the current detection model (YOLO or future variants).
        /// </summary>
        /// <param name="frameData">Raw pixel or preprocessed image data.</param>
        /// <returns>A list of DetectedObject entities.</returns>
        public static List<DetectedObject> ProcessFrame(byte[] frameData)
        {
            // Currently using YOLO only, but extendable for multiple backends
            return YoloDetector.ProcessFrame(frameData);
        }

        /// <summary>
        /// Switch to a different model backend (not yet implemented).
        /// </summary>
        /// <param name="modelName">New model identifier.</param>
        public static void SwitchModel(string modelName)
        {
            // Placeholder for switching between models like YOLOv8, SAM, or custom CNNs
            // Will tie into the ModelLoader and PredictionEngine
        }
    }
}
