//heavenly v3.0
using JaysAi.Finale.Modules;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.AI;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Aimbot
{
    public static class SnapLogic
    {
        public static void ProcessFrame()
        {
            if (!SnapConfig.Enabled || !InputManager.IsAiming())
                return;

            var targets = TargetingSystem.GetVisibleTargets();
            if (targets == null || targets.Count == 0)
                return;

            var bestTarget = TargetSelector.SelectBestTarget(targets);
            if (bestTarget == null)
                return;

            if (SnapConfig.DebugMode)
                OverlaySignal.SendSnapIntent(bestTarget);

            SnapController.ExecuteSnap(bestTarget);
        }
    }
}
