// neural v3.0
using System;
using System.Collections.Generic;
using JaysAi.Finale.AI;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.AI
{
    public class ModelBridge
    {
        private readonly IModelLoader _modelLoader;
        private bool _isInitialized;

        public ModelBridge(IModelLoader modelLoader)
        {
            _modelLoader = modelLoader;
        }

        public bool Initialize(string modelPath)
        {
            if (_isInitialized) return true;
            _isInitialized = _modelLoader.LoadModel(modelPath);
            return _isInitialized;
        }

        public List<YoloBoundingBox> RunInference(byte[] imageData, int width, int height)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("ModelBridge is not initialized.");

            var rawOutputs = _modelLoader.Infer(imageData, width, height);
            return YoloProcessor.ExtractBoundingBoxes(rawOutputs);
        }

        public void Dispose()
        {
            _modelLoader?.UnloadModel();
            _isInitialized = false;
        }
    }
}
