//monarch v2.1 – Visual Target Signal Bridge
using System;
using System.Collections.Generic;
using JaysAi.Finale.Visuals;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.AI
{
    public static class OverlaySignal
    {
        private static TrackedTarget _currentTarget = null;
        public static TrackedTarget CurrentTarget => _currentTarget;

        /// <summary>
        /// Sets a new tracked target from the ESP system.
        /// </summary>
        public static void PushTarget(TrackedTarget target)
        {
            if (target == null || !target.IsValid)
            {
                Logger.Log("[OverlaySignal] Ignoring invalid target push.", LogLevel.Debug);
                return;
            }

            _currentTarget = target;
            Logger.Log($"[OverlaySignal] New target acquired: ID={target.Id} @ ({target.X:F1}, {target.Y:F1})", LogLevel.Debug);
        }

        /// <summary>
        /// Clears current visual target. Used between frames or when enemies disappear.
        /// </summary>
        public static void Clear()
        {
            _currentTarget = null;
        }

        /// <summary>
        /// Checks if a target is active and lock-ready.
        /// </summary>
        public static bool HasTarget => _currentTarget != null && _currentTarget.IsValid;
    }
}
