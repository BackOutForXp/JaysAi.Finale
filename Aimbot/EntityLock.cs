// neural v3.0
using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Aimbot
{
    public class EntityLock
    {
        private TargetInfo currentLock;
        private DateTime lockStartTime;

        public bool IsLocked => currentLock != null && currentLock.IsAlive;

        public TargetInfo GetLockedTarget()
        {
            if (IsLocked && (DateTime.UtcNow - lockStartTime).TotalMilliseconds < 2000)
                return currentLock;

            ClearLock();
            return null;
        }

        public void LockOnto(TargetInfo target)
        {
            if (target == null || !target.IsAlive)
                return;

            currentLock = target;
            lockStartTime = DateTime.UtcNow;

            LogManager.Log($"[EntityLock] Locked onto target ID {target.Id}");
        }

        public void ClearLock()
        {
            currentLock = null;
        }

        public void UpdateLock(TargetInfo updatedTarget)
        {
            if (IsLocked && updatedTarget != null && updatedTarget.Id == currentLock.Id)
            {
                currentLock = updatedTarget;
            }
        }
    }
}
