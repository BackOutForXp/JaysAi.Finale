// Neural v3.1
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Data;
using JaysAi.Finale.Input;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Features
{
    public class AimAssist
    {
        private readonly TargetingSystem _targetingSystem;
        private readonly PredictionEngine _predictionEngine;

        public bool IsEnabled => FeatureToggleManager.IsEnabled("AimAssist");

        public AimAssist(TargetingSystem targetingSystem, PredictionEngine predictionEngine)
        {
            _targetingSystem = targetingSystem;
            _predictionEngine = predictionEngine;
        }

        public void Update()
        {
            if (!IsEnabled) return;

            var target = _targetingSystem.GetPrimaryTarget();
            if (target == null || !target.IsVisible || !target.ScreenPosition.HasValue)
                return;

            var predicted = _predictionEngine.GetPredictedPosition(target);
            if (predicted.HasValue)
            {
                // Use predicted aim smoothing or fallback to basic smoothing
                InputEmulator.MoveMouseTo(target, UserSettings.Instance.Get("AimSmoothing", 0.25f));
            }
        }
    }
}
