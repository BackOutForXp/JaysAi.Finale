// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Features;
using JaysAi.Finale.Input;
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
            _predictionEngine.UpdatePredictions(_tracker.GetTargets());
            _motionTracker.ProcessMovementData();
            _targetingSystem.EvaluateTargets(_tracker.GetTargets());
            _behaviorTrigger.Evaluate(_tracker.GetTargets());
            _runtimeLog.LogUpdate(_tracker.GetTargets(), _predictionEngine.LatestPredictions);
            _aiOverlay.UpdateOverlayData(_tracker.GetTargets());
        }

        public void Shutdown()
        {
            _aiOverlay.Unbind();
            _runtimeLog.EndSession();
            LogManager.Log("AiManager shut down.");
        }

        public List<TrackedTarget> GetCurrentTargets()
        {
            return _tracker.GetTargets();
        }

        public TrackedTarget GetPrimaryTarget()
        {
            return _targetingSystem.GetPrimaryTarget();
        }
    }
}
