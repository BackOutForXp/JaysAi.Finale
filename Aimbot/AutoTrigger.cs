//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Aimbot
{
    public static class AutoTrigger
    {
        private static bool _isEnabled = true;
        private static DateTime _lastTriggerTime = DateTime.MinValue;
        private static readonly int _triggerDelayMs = 50;

        public static void SetEnabled(bool enabled) => _isEnabled = enabled;

        public static void Update(TargetInfo target)
        {
            if (!_isEnabled || target == null || !target.IsValid)
                return;

            if (!IsCrosshairAligned(target))
                return;

            var now = DateTime.UtcNow;
            if ((now - _lastTriggerTime).TotalMilliseconds < _triggerDelayMs)
                return;

            InputInjector.PressFire();
            _lastTriggerTime = now;

            Logger.LogInfo($"[AutoTrigger] Fired at target: {target.Name}");
        }

        private static bool IsCrosshairAligned(TargetInfo target)
        {
            // Simple 2D check, refine with dot product if needed later
            var screenCenter = ScreenUtils.GetCenter();
            return Math.Abs(target.ScreenX - screenCenter.X) < 5 &&
                   Math.Abs(target.ScreenY - screenCenter.Y) < 5;
        }
    }
}
