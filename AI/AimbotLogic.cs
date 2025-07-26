// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aim;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class AimbotLogic
    {
        private readonly TargetingSystem _targetingSystem;
        private readonly PredictionEngine _predictionEngine;
        private readonly InputInjector _inputInjector;
        private readonly RecoilHandler _recoilHandler;
        private readonly AimSmoother _smoother;
        private readonly PIDController _pid;
        private readonly MotionTracker _motionTracker;

        public bool IsEnabled { get; set; } = true;

        public AimbotLogic()
        {
            _targetingSystem = new TargetingSystem();
            _predictionEngine = new PredictionEngine();
            _inputInjector = new InputInjector();
            _recoilHandler = new RecoilHandler();
            _smoother = new AimSmoother();
            _pid = new PIDController();
            _motionTracker = new MotionTracker();
        }

        public void Initialize()
        {
            _targetingSystem.Initialize();
            _predictionEngine.Initialize();
            _motionTracker.Initialize();
            LogManager.Log("AimbotLogic initialized.");
        }

        public void Execute()
        {
            if (!IsEnabled) return;

            TrackedTarget target = _targetingSystem.GetPrimaryTarget();
            if (target == null || !target.IsValid) return;

            Vector2 predictedPos = _predictionEngine.PredictTargetPosition(target);
            Vector2 aimAdjustment = _smoother.CalculateSmoothAdjustment(predictedPos);
            Vector2 correctedAim = _pid.ApplyCorrection(aimAdjustment);

            _inputInjector.InjectAimCorrection(correctedAim);
            _recoilHandler.ApplyCompensation();
        }

        public void Disable()
        {
            IsEnabled = false;
            LogManager.Log("AimbotLogic disabled.");
        }

        public void Enable()
        {
            IsEnabled = true;
            LogManager.Log("AimbotLogic enabled.");
        }
    }
}
