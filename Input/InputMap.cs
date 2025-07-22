//heavenly v3.0 – InputMap
using System.Collections.Generic;
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public static class InputMap
    {
        public static Dictionary<string, Key> KeyboardBinds { get; private set; }
        public static Dictionary<string, ControllerButton> ControllerBinds { get; private set; }

        static InputMap()
        {
            LoadDefaults();
        }

        public static void LoadDefaults()
        {
            KeyboardBinds = new Dictionary<string, Key>
            {
                { "ToggleESP", Key.F1 },
                { "ToggleAimAssist", Key.F2 },
                { "ToggleStickAssist", Key.F3 },
                { "ToggleRecoil", Key.F4 },
                { "ActivateTriggerBot", Key.LeftCtrl },
                { "OverrideAim", Key.LeftShift }
            };

            ControllerBinds = new Dictionary<string, ControllerButton>
            {
                { "ToggleESP", ControllerButton.DPadUp },
                { "ToggleAimAssist", ControllerButton.DPadRight },
                { "ToggleStickAssist", ControllerButton.DPadDown },
                { "ActivateTriggerBot", ControllerButton.RightTrigger },
                { "OverrideAim", ControllerButton.LeftTrigger }
            };
        }

        public static void RemapKey(string action, Key newKey)
        {
            if (KeyboardBinds.ContainsKey(action))
                KeyboardBinds[action] = newKey;
        }

        public static void RemapControllerButton(string action, ControllerButton newButton)
        {
            if (ControllerBinds.ContainsKey(action))
                ControllerBinds[action] = newButton;
        }
    }

    public enum ControllerButton
    {
        A,
        B,
        X,
        Y,
        LeftBumper,
        RightBumper,
        LeftTrigger,
        RightTrigger,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        Start,
        Select,
        LeftStick,
        RightStick
    }
}
