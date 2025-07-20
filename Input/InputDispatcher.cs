//monarch v2.1 – Routes real-time key presses to system toggles

using global::System;

namespace JaysAi.Finale.Input
{
    public static class InputDispatcher
    {
        public static KeyBindProfile ActiveProfile { get; set; } = new();

        public static Action OnToggleESP;
        public static Action OnToggleAimbot;
        public static Action OnToggleSnap;

        public static void CheckInputs()
        {
            if (ActiveProfile.IsBindPressed("ESP")) OnToggleESP?.Invoke();
            if (ActiveProfile.IsBindPressed("Aimbot")) OnToggleAimbot?.Invoke();
            if (ActiveProfile.IsBindPressed("Snap")) OnToggleSnap?.Invoke();
        }
    }
}
