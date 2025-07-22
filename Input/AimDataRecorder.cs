//heavenly v3.0 – Aim Data Telemetry Logger
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Input
{
    public class AimDataRecorder
    {
        private readonly List<string> _dataPoints = new();
        private readonly string _logPath;

        public AimDataRecorder()
        {
            _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"aim_data_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
            Directory.CreateDirectory(Path.GetDirectoryName(_logPath)!);
            _dataPoints.Add("Timestamp,TargetX,TargetY,CrosshairX,CrosshairY,DeltaX,DeltaY");
        }

        public void RecordFrame(Point target, Point crosshair)
        {
            var deltaX = target.X - crosshair.X;
            var deltaY = target.Y - crosshair.Y;
            var logEntry = $"{DateTime.UtcNow:O},{target.X:F2},{target.Y:F2},{crosshair.X:F2},{crosshair.Y:F2},{deltaX:F2},{deltaY:F2}";
            _dataPoints.Add(logEntry);
        }

        public void Save()
        {
            try
            {
                File.WriteAllLines(_logPath, _dataPoints, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AimDataRecorder] Failed to write aim log: {ex.Message}");
            }
        }
    }
}
