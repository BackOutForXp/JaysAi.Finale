// neural v3.0
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class YOLOBridge
    {
        private readonly string pythonScriptPath;
        private readonly string imageInputPath;
        private readonly string detectionOutputPath;

        public YOLOBridge(string scriptPath, string inputPath, string outputPath)
        {
            pythonScriptPath = scriptPath;
            imageInputPath = inputPath;
            detectionOutputPath = outputPath;
        }

        public List<YoloBoundingBox> RunDetection(Mat frame)
        {
            // Save frame as image
            Cv2.ImWrite(imageInputPath, frame);

            // Run the Python script
            using (var process = new Process())
            {
                process.StartInfo.FileName = "python";
                process.StartInfo.Arguments = $"\"{pythonScriptPath}\" \"{imageInputPath}\" \"{detectionOutputPath}\"";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                process.WaitForExit();
            }

            return ParseDetectionOutput();
        }

        private List<YoloBoundingBox> ParseDetectionOutput()
        {
            var boxes = new List<YoloBoundingBox>();
            if (!File.Exists(detectionOutputPath)) return boxes;

            try
            {
                var json = File.ReadAllText(detectionOutputPath);
                var detections = JsonSerializer.Deserialize<List<YoloDetectionResult>>(json);

                foreach (var det in detections)
                {
                    boxes.Add(new YoloBoundingBox(
                        det.ClassId,
                        det.Label,
                        det.Confidence,
                        new Rect(det.X, det.Y, det.Width, det.Height)
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[YOLOBridge] Error parsing detection output: {ex.Message}");
            }

            return boxes;
        }

        private class YoloDetectionResult
        {
            public int ClassId { get; set; }
            public string Label { get; set; }
            public float Confidence { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
    }
}
