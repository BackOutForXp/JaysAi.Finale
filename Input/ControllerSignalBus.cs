// Monarch v1.0 – ControllerSignalBus.cs
// ✅ Monarch Fix Checklist
// [x] Accepts analog input for movement
// [x] Future-proofed for external device APIs
// [x] Easy to route to Zen, Titan Two, or custom emulators

using System;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Input
{
    public static class ControllerSignalBus
    {
        public static void SendAnalogInput(short x, short y)
        {
            // Placeholder: send to virtual controller or external bridge
            // Replace with actual injection logic for Cronus/TitanTwo/ViGEm/etc.

            Logger.Log($"[SignalBus] Analog input: X={x} Y={y}");

            // Example for debugging only — no real input yet
            // Actual implementation may require HID driver or ZenScript output
        }

        public static void SendButtonPress(string buttonName)
        {
            // Map string buttons like "RT", "LT", "A", etc.
            Logger.Log($"[SignalBus] Button pressed: {buttonName}");

            // TODO: Send real input to driver/device
        }
    }
}
