// Monarch v1.0 – YoloDetector.cs
// ✅ Monarch Fix Checklist
// [x] Runs model inference from screen frame
// [x] Parses raw output to bounding boxes
// [x] Filters by confidence threshold and class labels

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JaysAi.Finale.Structs;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class YoloDetector
    {
        private readonly InferenceSession _session;
        private readonly float _confidenceThreshold = 0.5f;
        private readonly string[] _classLabels = new[] { "person", "enemy", "object" };

        public YoloDetector(InferenceSession session)
        {
            _session = session;
        }

        public List<ESPObject> Detect(Mat inputFrame)
        {
            var resized = inputFrame.Resize(new OpenCvSharp.Size(640, 640));
            var tensor = CreateInputTensor(resized);

            using var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("images", tensor)
            };

            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = _session.Run(inputs);
            return ParseResults(results, inputFrame.Width, inputFrame.Height);
        }

        private DenseTensor<float> CreateInputTensor(Mat frame)
        {
            var chw = new float[1, 3, 640, 640];
            var data = frame.ToBytes();

            for (int y = 0; y < frame.Rows; y++)
            {
                for (int x = 0; x < frame.Cols; x++)
                {
                    var color = frame.At<Vec3b>(y, x);
                    chw[0, 0, y, x] = color.Item2 / 255f; // R
                    chw[0, 1, y, x] = color.Item1 / 255f; // G
                    chw[0, 2, y, x] = color.Item0 / 255f; // B
                }
            }

            return new DenseTensor<float>(chw, new[] { 1, 3, 640, 640 });
        }

        private List<ESPObject> ParseResults(IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results, int width, int height)
        {
            var boxes = new List<ESPObject>();

            foreach (var result in results)
            {
                var output = result.AsTensor<float>();
                var dims = output.Dimensions;

                for (int i = 0; i < dims[1]; i++)
                {
                    float conf = output[0, i, 4];
                    if (conf < _confidenceThreshold)
                        continue;

                    int classId = Array.IndexOf(output[0, i].Skip(5).ToArray(), output[0, i].Skip(5).Max());

                    float centerX = output[0, i, 0] * width;
                    float centerY = output[0, i, 1] * height;
                    float boxW = output[0, i, 2] * width;
                    float boxH = output[0, i, 3] * height;

                    var esp = new ESPObject
                    {
                        X = centerX - boxW / 2,
                        Y = centerY - boxH / 2,
                        Width = boxW,
                        Height = boxH,
                        Label = _classLabels.ElementAtOrDefault(classId),
                        IsEnemy = classId == 0 // e.g., person
                    };

                    boxes.Add(esp);
                }
            }

            return boxes;
        }
    }
}
