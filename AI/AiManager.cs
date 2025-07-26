// neural v3.1
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.Features;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class AiManager
    {
        private readonly PredictionEngine _predictionEngine;
        private readonly TargetingSystem _targetingSystem;
        private readonly BehaviorTrigger _behaviorTrigger;
        private readonly RuntimeBehaviorLog _runtimeLog;
        private readonly TrackTarget _tracker;
        private readonly MotionTracker _motionTracker;
        private readonly AiOverlay _aiOverlay;

        public AiManager()
        {
            _predictionEngine = new PredictionEngine();
            _targetingSystem = new TargetingSystem();
            _behaviorTrigger = new BehaviorTrigger();
            _runtimeLog = new RuntimeBehaviorLog();
            _tracker = new TrackTarget();
            _motionTracker = new MotionTracker();
            _aiOverlay = new AiOverlay();
        }

        public void Initialize()
        {
            _predictionEngine.Initialize();
            _tracker.Initialize();
            _targetingSystem.Initialize();
            _motionTracker.Initialize();
            _behaviorTrigger.RegisterTriggers();
            _runtimeLog.StartSession();
            _aiOverlay.BindToAI(this);

            LogManager.Log("AiManager initialized successfully.");
        }

        public void Update()
        {
            _tracker.UpdateTracking();

            var targets = _tracker.GetTargets() ?? new List<TrackedTarget>();

            _predictionEngine.UpdatePredictions(targets);
            _motionTracker.ProcessMovementData();
            _targetingSystem.EvaluateTargets(targets);
            _behaviorTrigger.Evaluate(targets);
            _runtimeLog.LogUpdate(targets, _predictionEngine.LatestPredictions);
            _aiOverlay.UpdateOverlayData(targets);
        }

        public void Shutdown()
        {
            _aiOverlay.Unbind();
            _runtimeLog.EndSession();
            LogManager.Log("AiManager shut down.");
        }

        public List<TrackedTarget> GetCurrentTargets() => _tracker.GetTargets();

        public TrackedTarget GetPrimaryTarget() => _targetingSystem.GetPrimaryTarget();
    }
}
