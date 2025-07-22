//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Aimbot
{
    public static class EntityLock
    {
        private static TargetInfo _lockedTarget;
        private static float _lockDurationSeconds = 2f;
        private static DateTime _lastLockTime = DateTime.MinValue;

        public static TargetInfo Current => _lockedTarget;

        public static void AttemptLock(TargetInfo newTarget)
        {
            if (newTarget != null && newTarget.IsValid)
            {
                _lockedTarget = newTarget;
                _lastLockTime = DateTime.UtcNow;
                Logger.LogInfo($"[EntityLock] Locked onto {newTarget.Name}");
            }
        }

        public static void UpdateLock()
        {
            if (_lockedTarget == null)
                return;

            if (!_lockedTarget.IsValid || IsLockExpired())
            {
                Logger.LogDebug($"[EntityLock] Lock expired or target lost.");
                _lockedTarget = null;
            }
        }

        private static bool IsLockExpired()
        {
            return (DateTime.UtcNow - _lastLockTime).TotalSeconds > _lockDurationSeconds;
        }

        public static bool HasLock => _lockedTarget != null && _lockedTarget.IsValid;
    }
}
