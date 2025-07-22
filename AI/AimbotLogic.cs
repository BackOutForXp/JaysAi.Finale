//heavenly v3.0 – Aimbot Logic Core Frame Handler
using JaysAi.Finale.Modules;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Input;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public static class AimbotLogic
    {
        private static bool _isInitialized;

        public static void Initialize()
        {
            if (_isInitialized) return;

            PredictionEngine.Initialize();
            InputDispatcher.Initialize();
            SnapAssist.Initialize();
            _isInitialized = true;
        }

        public static void Update()
        {
            if (!_isInitialized)
                Initialize();

            // Step 1: Get current input and frame data
            var inputState = InputDispatcher.GetCurrentState();
            var trackedTargets = TargetingSystem.GetValidTargets();

            if (trackedTargets == null || trackedTargets.Count == 0)
                return;

            // Step 2: Score and select best target
            var bestTarget = TargetSelector.GetBestTarget(trackedTargets);

            if (bestTarget == null)
                return;

            // Step 3: Snap logic or predictive pathing
            if (AppSettings.Aim.UsePrediction)
            {
                var predictedPos = PredictionEngine.Predict(bestTarget);
                if (AppSettings.Aim.SnapEnabled)
                    SnapAssist.LockOn(predictedPos);
            }
            else
            {
                SnapAssist.LockOn(bestTarget);
            }

            // Step 4: Trigger bot support (optional)
            if (AppSettings.Aim.AutoTrigger && bestTarget.IsInCrosshair)
                AutoTrigger.TryFire(bestTarget);

            // Step 5: Log or store behavior
            RuntimeBehaviorLog.Log(bestTarget);
        }
    }
}
