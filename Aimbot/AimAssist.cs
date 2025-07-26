// neural v3.0
using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Aim;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Data;

namespace JaysAi.Finale.Aimbot
{
    public class AimAssist
    {
        private readonly PredictionEngine predictionEngine;
        private readonly PIDController aimSmoother;
        private readonly TargetSelector targetSelector;
        private readonly InputInjector inputInjector;
        private SnapTarget? currentTarget;
        private bool isActive;

        public AimAssist(PredictionEngine predictionEngine, TargetSelector targetSelector, InputInjector injector)
        {
            this.predictionEngine = predictionEngine;
            this.targetSelector = targetSelector;
            this.inputInjector = injector;
            this.aimSmoother = new PIDController(0.6f, 0.01f, 0.05f);
            this.isActive = true;
        }

        public void Toggle(bool state)
        {
            isActive = state;
        }

        public void Update()
        {
            if (!isActive)
                return;

            currentTarget = targetSelector.GetBestTarget();

            if (currentTarget == null || !currentTarget.Value.IsValid())
                return;

            Vector2 predictedPosition = predictionEngine.PredictTargetPosition(currentTarget.Value);
            Vector2 aimOffset = predictionEngine.CalculateAimOffset(predictedPosition);

            Vector2 smoothOffset = aimSmoother.Smooth(aimOffset);
            inputInjector.MoveCursor(smoothOffset);
        }
    }
}
