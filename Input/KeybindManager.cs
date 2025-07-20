//monarch v2.1 – Key Bind Manager
using System.Collections.Generic;
using System.Windows.Input;

namespace JaysAi.Finale.Utility
{
    public static class KeyBindManager
    {
        // Default Keybinds (can be expanded later)
        private static readonly Dictionary<string, Key> keyBindings = new()
        {
            { "ToggleESP", Key.F1 },
            { "ToggleAimbot", Key.F2 },
            { "ToggleSnapAssist", Key.F3 },
            { "ToggleStealthMode", Key.F4 },
            { "ReloadConfig", Key.F5 },
            { "Panic", Key.Delete }
        };

        public static bool IsKeyPressed(string action)
        {
            if (!keyBindings.ContainsKey(action)) return false;
            return Keyboard.IsKeyDown(keyBindings[action]);
        }

        public static Key GetKeyBind(string action)
        {
            return keyBindings.ContainsKey(action) ? keyBindings[action] : Key.None;
        }

        public static void SetKeyBind(string action, Key newKey)
        {
            if (keyBindings.ContainsKey(action))
                keyBindings[action] = newKey;
            else
                keyBindings.Add(action, newKey);
        }

        public static Dictionary<string, Key> GetAllBindings()
        {
            return new Dictionary<string, Key>(keyBindings);
        }
    }
}
 