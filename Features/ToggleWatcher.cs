// Neural v3.1 — ToggleWatcher.cs
using JaysAi.Finale.Input;
using JaysAi.Finale.Settings;
using OpenCvSharp.LineDescriptor;
using System.Collections.Generic;

namespace JaysAi.Finale.Features
{
    public class ToggleWatcher
    {
        private readonly Dictionary<string, Keybind> _toggles = new();
        private readonly Dictionary<string, bool> _toggleStates = new();

        public void RegisterToggle(string name, Keybind keybind)
        {
            _toggles[name] = keybind;
            if (!_toggleStates.ContainsKey(name))
                _toggleStates[name] = false;
        }

        public void Update()
        {
            foreach (var pair in _toggles)
            {
                var name = pair.Key;
                var keybind = pair.Value;

                if (InputManager.IsKeyPressed(keybind.Key))
                {
                    _toggleStates[name] = !_toggleStates[name];
                }
            }
        }

        public bool IsEnabled(string name)
        {
            return _toggleStates.TryGetValue(name, out var enabled) && enabled;
        }

        public void SetState(string name, bool value)
        {
            _toggleStates[name] = value;
        }
    }
}
