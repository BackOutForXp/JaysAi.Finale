//heavenly v3.0 – Model Loader & Detection Manager
using System;
using System.Collections.Generic;
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public static class ModelLoader
    {
        private static readonly Dictionary<string, IDetectionModel> _models = new();
        private static IDetectionModel _activeModel;

        public static void RegisterModel(string name, IDetectionModel model)
        {
            if (!_models.ContainsKey(name))
            {
                _models.Add(name, model);
                LogManager.LogInfo($"Registered AI model: {name}");
            }
        }

        public static void LoadModel(string name)
        {
            if (_models.TryGetValue(name, out var model))
            {
                _activeModel = model;
                _activeModel.Load();
                LogManager.LogInfo($"Loaded AI model: {name}");
            }
            else
            {
                LogManager.LogError($"Model not found: {name}");
            }
        }

        public static List<DetectedObject> RunDetection(FrameSnapshot frame)
        {
            if (_activeModel == null)
            {
                LogManager.LogError("No active AI model set.");
                return new List<DetectedObject>();
            }

            return _activeModel.Detect(frame);
        }

        public static IEnumerable<string> GetAvailableModels() => _models.Keys;

        public static void UnloadCurrentModel()
        {
            _activeModel?.Unload();
            _activeModel = null;
            LogManager.LogInfo("Unloaded current AI model.");
        }

        public static bool HasActiveModel => _activeModel != null;
    }

    public interface IDetectionModel
    {
        void Load();
        void Unload();
        List<DetectedObject> Detect(FrameSnapshot frame);
    }
}
