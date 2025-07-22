//heavenly v3.0 – Adaptive Aim Correction Engine
using System;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public static class MonarchAimAI
    {
        private static TrackedTarget _currentTarget;
        private static DateTime _lastEngagedTime;

        public static void EvaluateAndEngage(TrackedTarget target)
        {
            if (target == null || !target.IsValid) return;

            _currentTarget = target;
            _lastEngagedTime = DateTime.UtcNow;

            var predicted = PredictionEngine.Predict(target);
            if (predicted != null)
            {
                SnapAssist.LockOn(predicted);
                LogManager.LogDebug($"Engaged target: {target.Id} using prediction.");
            }
        }

        public static void Update()
        {
            if (_currentTarget == null || !_currentTarget.IsValid)
                return;

            var timeSinceEngage = DateTime.UtcNow - _lastEngagedTime;
            if (timeSinceEngage.TotalMilliseconds > 750)
            {
                LogManager.LogInfo($"Target timeout: {_currentTarget.Id}");
                _currentTarget = null;
                return;
            }

            EvaluateAndEngage(_currentTarget);
        }

        public static void Reset()
        {
            _currentTarget = null;
        }

        public static bool HasTarget => _currentTarget != null;
    }
}
