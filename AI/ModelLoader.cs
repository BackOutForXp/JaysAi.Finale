//monarch v2.1
using System;
using System.IO;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace JaysAi.Finale.AI
{
    public class ModelLoader
    {
        public InferenceSession Session { get; private set; }
        public string ModelPath { get; private set; }

        public ModelLoader(string modelPath)
        {
            if (!File.Exists(modelPath))
                throw new FileNotFoundException($"Model file not found: {modelPath}");

            ModelPath = modelPath;

            var options = new SessionOptions();
            options.LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_WARNING;

            // Enable CPU by default, can be upgraded later for GPU
            options.AppendExecutionProvider_CPU();

            Session = new InferenceSession(ModelPath, options);
        }

        public IDisposableReadOnlyCollection<DisposableNamedOnnxValue> RunInference(DenseTensor<float> input)
        {
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("images", input)
            };

            return Session.Run(inputs);
        }

        public void Dispose()
        {
            Session?.Dispose();
        }
    }
}
