// Neural v3.1
using JaysAi.Finale.Data;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JaysAi.Finale.AI
{
    public class RuntimeBehaviorLog
    {
        private readonly StringBuilder _logBuilder = new();
        private string _logPath = "";
        private DateTime _startTime;

        public void StartSession()
        {
            _startTime = DateTime.Now;
            var filename = $"behavior-log-{_startTime:yyyyMMdd-HHmmss}.txt";
            _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", filename);

            Directory.CreateDirectory(Path.GetDirectoryName(_logPath)!);
            _logBuilder.Clear();
            _logBuilder.AppendLine($"=== AI Session Started: {_startTime} ===");
        }

        public void LogUpdate(List<TrackedTarget> targets, List<PredictedTarget> predictions)
        {
            _logBuilder.AppendLine($"[{DateTime.Now:HH:mm:ss.fff}]");

            for (int i = 0; i < targets.Count; i++)
            {
                var target = targets[i];
                var prediction = i < predictions.Count ? predictions[i] : null;

                _logBuilder.AppendLine(
                    $"- Target {target.Id}: FOV={target.FovDistance:F1}, " +
                    $"Conf={target.Confidence:F2}, Predicted={prediction?.PredictedPosition}");
            }

            _logBuilder.AppendLine();
        }

        public void EndSession()
        {
            _logBuilder.AppendLine($"=== Session Ended: {DateTime.Now} ===");

            try
            {
                File.WriteAllText(_logPath, _logBuilder.ToString());
            }
            catch (Exception ex)
            {
                LogManager.LogError("Failed to write behavior log", ex);
            }
        }
    }
}
