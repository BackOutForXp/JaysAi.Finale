//monarch v2.1
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class ModelBridge
    {
        private readonly int inputWidth;
        private readonly int inputHeight;

        public ModelBridge(int width = 320, int height = 320)
        {
            inputWidth = width;
            inputHeight = height;
        }

        public DenseTensor<float> ConvertFrameToTensor(SKBitmap frame)
        {
            var tensor = new DenseTensor<float>(new[] { 1, 3, inputHeight, inputWidth });

            for (int y = 0; y < inputHeight; y++)
            {
                for (int x = 0; x < inputWidth; x++)
                {
                    var color = frame.GetPixel(x, y);

                    tensor[0, 0, y, x] = color.Red / 255f;
                    tensor[0, 1, y, x] = color.Green / 255f;
                    tensor[0, 2, y, x] = color.Blue / 255f;
                }
            }

            return tensor;
        }

        public List<PredictionResult> ParseModelOutput(IEnumerable<NamedOnnxValue> results)
        {
            var output = results.First().AsEnumerable<float>().ToArray();
            var predictions = new List<PredictionResult>();

            int stride = 6; // [x1, y1, x2, y2, conf, class]
            for (int i = 0; i < output.Length; i += stride)
            {
                var x1 = output[i];
                var y1 = output[i + 1];
                var x2 = output[i + 2];
                var y2 = output[i + 3];
                var conf = output[i + 4];
                var classId = (int)output[i + 5];

                if (conf < 0.5f)
                    continue;

                var center = new Vector2((x1 + x2) / 2, (y1 + y2) / 2);

                predictions.Add(new PredictionResult
                {
                    BoundingBox = new SKRect(x1, y1, x2, y2),
                    Confidence = conf,
                    Label = $"Class {classId}",
                    Id = Guid.NewGuid().ToString(),
                    IsOnScreen = true,
                    ScreenPosition = center
                });
            }

            return predictions;
        }
    }
}
