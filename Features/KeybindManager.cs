// neural v3.0
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System.Collections.Generic;
using System.Windows.Input;

namespace JaysAi.Finale.Features
{
    public static class KeybindManager
    {
        private static readonly Dictionary<string, Key> BoundKeys = new();

        public static void Initialize()
        {
            Bind("ToggleESP", UserSettings.Current.ESPKey);
            Bind("ToggleAimAssist", UserSettings.Current.AimAssistKey);
            Bind("ToggleSnap", UserSettings.Current.SnapKey);
            Bind("ToggleStealth", UserSettings.Current.StealthModeKey);
        }

        public static void Bind(string action, Key key)
        {
            if (BoundKeys.ContainsKey(action))
                BoundKeys[action] = key;
            else
                BoundKeys.Add(action, key);
        }

        public static bool IsPressed(string action)
        {
            return BoundKeys.TryGetValue(action, out var key) && Keyboard.IsKeyDown(key);
        }

        public static void Update()
        {
            if (IsPressed("ToggleESP"))
                FeatureToggleManager.ToggleESP();

            if (IsPressed("ToggleAimAssist"))
                FeatureToggleManager.ToggleAimAssist();

            if (IsPressed("ToggleSnap"))
                FeatureToggleManager.ToggleSnap();

            if (IsPressed("ToggleStealth"))
                FeatureToggleManager.ToggleStealth();
        }
    }
}
