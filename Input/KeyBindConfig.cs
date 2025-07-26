// neural v3.0
using System.Collections.Generic;

namespace JaysAi.Finale.Input
{
    public class KeybindConfig
    {
        public Dictionary<string, InputBinding> Bindings { get; } = new();

        public void SetBinding(string actionName, InputBinding binding)
        {
            if (Bindings.ContainsKey(actionName))
                Bindings[actionName] = binding;
            else
                Bindings.Add(actionName, binding);
        }

        public bool TryGetBinding(string actionName, out InputBinding binding)
        {
            return Bindings.TryGetValue(actionName, out binding);
        }

        public void Clear()
        {
            Bindings.Clear();
        }

        public IEnumerable<string> GetAllActions()
        {
            return Bindings.Keys;
        }
    }

    public class InputBinding
    {
        public string Device { get; set; } = "Keyboard"; // or "Controller"
        public string Key { get; set; } = string.Empty;
        public bool IsToggle { get; set; } = false;

        public override string ToString()
        {
            return $"{Device}:{Key}" + (IsToggle ? " [Toggle]" : "");
        }
    }
}
