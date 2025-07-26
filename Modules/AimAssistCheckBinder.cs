// neural v3.0
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Modules
{
    public static class AimAssistCheckBinder
    {
        private static bool _isInitialized;
        private static Func<bool>? _customEligibilityCheck;

        public static void Initialize(Func<bool>? overrideCheck = null)
        {
            if (_isInitialized) return;

            _customEligibilityCheck = overrideCheck;
            Logger.Log("AimAssistCheckBinder initialized.");
            _isInitialized = true;
        }

        public static bool ShouldEnableAimAssist()
        {
            try
            {
                // Custom override logic provided externally
                if (_customEligibilityCheck != null)
                    return _customEligibilityCheck();

                // Fallback: use default settings and runtime context
                return AppSettings.Current.AimAssistEnabled &&
                       GameStateTracker.IsInMatch &&
                       !UserSessionManager.IsInMenu &&
                       !TargetLockManager.IsLockedOnFriendly;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed during AimAssist eligibility check.", ex);
                return false;
            }
        }
    }
}
