//monarch v2.1 – YOLO Detection Analyzer
using JaysAi.Finale.AI.Models;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaysAi.Finale.AI
{
    public class YoloDetector
    {
        private readonly YOLOBridge _bridge;
        private readonly float _minConfidence;
        private readonly string _enemyLabel;
        private readonly int _maxTargets;

        public List<YoloTarget> LastDetections { get; private set; }

        public YoloDetector(YOLOBridge bridge, float minConfidence = 0.55f, string enemyLabel = "enemy", int maxTargets = 5)
        {
            _bridge = bridge;
            _minConfidence = minConfidence;
            _enemyLabel = enemyLabel;
            _maxTargets = maxTargets;
        }

        public async Task<List<YoloTarget>> GetTargetsAsync(string imagePath)
        {
            var detections = await _bridge.AnalyzeAsync(imagePath);
            LastDetections = FilterTargets(detections);
            return LastDetections;
        }

        private List<YoloTarget> FilterTargets(List<YoloTarget> detections)
        {
            if (detections == null || detections.Count == 0)
                return new List<YoloTarget>();

            var filtered = detections
                .Where(d => d.Label.Equals(_enemyLabel, StringComparison.OrdinalIgnoreCase) && d.Confidence >= _minConfidence)
                .OrderByDescending(d => d.Confidence)
                .Take(_maxTargets)
                .ToList();

            Logger.Debug($"YOLO: {filtered.Count} targets passed filtering.");
            return filtered;
        }

        public void DrawDebugOverlay()
        {
            if (LastDetections == null || LastDetections.Count == 0)
                return;

            foreach (var target in LastDetections)
                OverlaySignal.SendBox(target.X, target.Y, target.Width, target.Height, label: "Target");
        }
    }
}
