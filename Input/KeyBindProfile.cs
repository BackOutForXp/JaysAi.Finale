//monarch v2.1 – Keybind profile for feature mapping

using global::System.Collections.Generic;
using global::System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public class KeyBindProfile
    {
        public Dictionary<string, KeyBindConfig> KeyBinds { get; } = new();

        public void AddBind(string name, Key key, bool toggleMode = false)
        {
            KeyBinds[name] = new KeyBindConfig(name, key, toggleMode);
        }

        public bool IsBindPressed(string name)
        {
            if (!KeyBinds.TryGetValue(name, out var bind))
                return false;

            return Keyboard.IsKeyDown(bind.Key);
        }
    }
}
