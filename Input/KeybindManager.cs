// neural v3.0
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public sealed class KeybindManager
    {
        private static readonly Lazy<KeybindManager> _instance = new(() => new KeybindManager());
        public static KeybindManager Instance => _instance.Value;

        private readonly KeybindConfig _config;
        private readonly Dictionary<string, Action> _bindings = new();

        private KeybindManager()
        {
            _config = new KeybindConfig();
        }

        public void RegisterAction(string actionName, Action callback)
        {
            if (!_bindings.ContainsKey(actionName))
                _bindings.Add(actionName, callback);
        }

        public void BindKey(string actionName, Key key, string device = "Keyboard", bool isToggle = false)
        {
            var binding = new InputBinding
            {
                Device = device,
                Key = key.ToString(),
                IsToggle = isToggle
            };

            _config.SetBinding(actionName, binding);
        }

        public void HandleKeyInput(Key keyPressed)
        {
            foreach (var kvp in _config.Bindings)
            {
                var binding = kvp.Value;
                if (binding.Device != "Keyboard") continue;
                if (binding.Key.Equals(keyPressed.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    if (_bindings.TryGetValue(kvp.Key, out var action))
                        action?.Invoke();
                }
            }
        }

        public InputBinding? GetBinding(string actionName)
        {
            return _config.TryGetBinding(actionName, out var binding) ? binding : null;
        }

        public void ClearAll()
        {
            _config.Clear();
            _bindings.Clear();
        }
    }
}
