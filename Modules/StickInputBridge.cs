// Monarch v1.0 – StickInputBridge.cs
// ✅ Monarch Fix Checklist
// [x] Accepts Vector2 input from AimbotLogic
// [x] Injects smooth stick movement
// [x] Includes magnitude clamping for legit-looking aim

using System;
using System.Numerics;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Modules
{
    public static class StickInputBridge
    {
        private const float MaxMagnitude = 100f; // Max movement allowed per tick

        public static void ApplyOffset(Vector2 offset)
        {
            if (offset == Vector2.Zero)
                return;

            // Clamp magnitude to prevent teleporting snap
            if (offset.Length() > MaxMagnitude)
                offset = Vector2.Normalize(offset) * MaxMagnitude;

            // Optional: scale to controller range (0–100 or -100 to 100)
            short stickX = (short)Math.Clamp(offset.X, -100f, 100f);
            short stickY = (short)Math.Clamp(offset.Y, -100f, 100f);

            ControllerSignalBus.SendAnalogInput(stickX, stickY);
            Logger.Log($"[Stick] Injected aim offset X:{stickX} Y:{stickY}");
        }
    }
}
