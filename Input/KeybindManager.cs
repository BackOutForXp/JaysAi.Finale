// File: Input/KeybindManager.cs
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Input
{
    public class KeybindManager
    {
        private readonly Dictionary<int, Action> _bindings = new();
        private readonly HashSet<int> _cooldown = new();

        public KeybindManager()
        {
            InputLogger.OnKeyPressed += HandleKeyPress;
        }

        public void Bind(int keyCode, Action callback)
        {
            _bindings[keyCode] = callback;
        }

        public void Unbind(int keyCode)
        {
            if (_bindings.ContainsKey(keyCode))
                _bindings.Remove(keyCode);
        }

        private void HandleKeyPress(int keyCode)
        {
            if (_bindings.TryGetValue(keyCode, out var callback))
            {
                if (!_cooldown.Contains(keyCode))
                {
                    callback.Invoke();
                    _cooldown.Add(keyCode);
                }
            }
        }

        public void Update()
        {
            // Clear cooldown on keys no longer pressed
            foreach (var key in new List<int>(_cooldown))
            {
                if (!InputLogger.IsKeyDown(key))
                    _cooldown.Remove(key);
            }
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Clean keybind logic with no dependency on Forms
// ✅ Supports multiple hotkey bindings
// ✅ Works with InputLogger to monitor key presses
// TODO: Add support for modifier keys (Ctrl/Alt/Shift)
// TODO: Add string-based key mapping support (e.g., "F1", "A", etc.)
// ===================================================================
