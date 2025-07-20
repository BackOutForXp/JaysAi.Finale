// Monarch v1.0 – ControllerInputLogger.cs
// ✅ Monarch Fix Checklist
// [x] Tracks LT/RT trigger pressure
// [x] Can be expanded for full button/stick logging
// [x] Feeds into SnapAssist and AimbotLogic control checks

using System;

namespace JaysAi.Finale.Input
{
    public static class ControllerInputLogger
    {
        private static float leftTriggerPressure = 0f;
        private static float rightTriggerPressure = 0f;

        // Simulate update loop (normally called each tick/frame)
        public static void UpdateTriggerState(float lt, float rt)
        {
            leftTriggerPressure = Math.Clamp(lt, 0f, 1f);
            rightTriggerPressure = Math.Clamp(rt, 0f, 1f);
        }

        public static float GetTriggerPressure()
        {
            // Return whichever is stronger (LT or RT) to drive SnapTrigger activation
            return Math.Max(leftTriggerPressure, rightTriggerPressure);
        }

        public static bool IsAiming()
        {
            return leftTriggerPressure > 0.4f;
        }

        public static bool IsFiring()
        {
            return rightTriggerPressure > 0.4f;
        }
    }
}
