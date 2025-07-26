// neural v3.0
using JaysAi.Finale.Aim;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class MonarchAimAI
    {
        private readonly PredictionEngine _predictionEngine = new();
        private readonly AiMemory _aiMemory = new();
        private readonly TargetEvaluator _evaluator = new();
        private readonly RuntimeBehaviorLog _behaviorLog = new();

        private TrackedTarget _lastTarget;
        private float _correctionFactor = 1.0f;

        public MonarchAimAI()
        {
            Logger.Info("MonarchAimAI initialized.");
        }

        public TrackedTarget SelectTarget(List<YoloBoundingBox> detections, PlayerState player)
        {
            if (detections == null || detections.Count == 0) return null;

            var candidates = new List<TrackedTarget>();

            foreach (var box in detections)
            {
                var tracked = new TrackedTarget
                {
                    BoundingBox = ModelCalibration.ScaleBoundingBox(box),
                    Confidence = box.Confidence,
                    Label = box.Label,
                    Timestamp = TimeUtils.Now()
                };

                tracked.Score = _evaluator.Evaluate(tracked, player);
                candidates.Add(tracked);
            }

            candidates.Sort((a, b) => b.Score.CompareTo(a.Score));
            _lastTarget = candidates.Count > 0 ? candidates[0] : null;

            _aiMemory.UpdateRecentTarget(_lastTarget);
            return _lastTarget;
        }

        public AimAdjustment CalculateAimCorrection(TrackedTarget target, PlayerState playerState)
        {
            if (target == null) return new AimAdjustment { DeltaX = 0, DeltaY = 0 };

            var prediction = _predictionEngine.PredictTargetMovement(target, playerState);
            var adjustment = _predictionEngine.CalculateCorrection(playerState.AimPosition, prediction);

            adjustment.DeltaX *= _correctionFactor;
            adjustment.DeltaY *= _correctionFactor;

            _behaviorLog.LogAdjustment(target, adjustment);
            return adjustment;
        }

        public void LearnFromMistake(TrackedTarget target, bool wasHit)
        {
            if (!wasHit)
            {
                _correctionFactor *= 1.05f;
            }
            else
            {
                _correctionFactor *= 0.97f;
            }

            _correctionFactor = Math.Clamp(_correctionFactor, 0.7f, 1.3f);
        }

        public void Reset()
        {
            _correctionFactor = 1.0f;
            _lastTarget = null;
        }
    }

    public class AimAdjustment
    {
        public float DeltaX { get; set; }
        public float DeltaY { get; set; }
    }
}
