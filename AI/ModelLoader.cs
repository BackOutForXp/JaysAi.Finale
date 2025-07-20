//monarch v2.1 – YOLOv8 Model Loader
using System;
using System.IO;
using JaysAi.Finale.Utility;
using OpenCvSharp.Dnn;

namespace JaysAi.Finale.AI
{
    public class ModelLoader
    {
        private Net _net;
        private string _weightsPath;

        public bool IsLoaded => _net != null && !_net.Empty();

        public ModelLoader(string weightsFileName = "yolov8.onnx")
        {
            _weightsPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Models", weightsFileName);
        }

        public bool Load()
        {
            if (!File.Exists(_weightsPath))
            {
                Logger.Log($"Model file not found: {_weightsPath}", LogLevel.Error);
                return false;
            }

            try
            {
                _net = CvDnn.ReadNetFromONNX(_weightsPath);
                _net.SetPreferableBackend(Backend.OPENCV);
                _net.SetPreferableTarget(Target.CPU); // Optionally change to Target.CUDA if GPU is available
                Logger.Log("YOLOv8 model successfully loaded.", LogLevel.Info);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to load model: {ex.Message}", LogLevel.Error);
                return false;
            }
        }

        public Net GetNetwork()
        {
            return _net;
        }
    }
}
