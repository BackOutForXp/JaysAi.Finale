// neural v3.0
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public interface IModelLoader
    {
        bool LoadModel(string modelPath);
        float[] Infer(byte[] inputImage, int width, int height);
        void UnloadModel();
    }

    public class OnnxModelLoader : IModelLoader
    {
        private dynamic _session;
        private string _loadedModelPath;

        public bool LoadModel(string modelPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(modelPath)) return false;
                if (_loadedModelPath == modelPath) return true;

                _session = OnnxHelper.LoadSession(modelPath);
                _loadedModelPath = modelPath;
                Logger.Info($"Model loaded: {modelPath}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Model load failed: " + ex.Message);
                return false;
            }
        }

        public float[] Infer(byte[] inputImage, int width, int height)
        {
            if (_session == null)
                throw new InvalidOperationException("Model not loaded.");

            return OnnxHelper.RunInference(_session, inputImage, width, height);
        }

        public void UnloadModel()
        {
            _session?.Dispose();
            _session = null;
            _loadedModelPath = null;
            Logger.Info("Model unloaded.");
        }
    }
}
