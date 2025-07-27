using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public class KeybindWatcher
    {
        private readonly Dictionary<Key, Action> _bindings = new();
        private readonly HashSet<Key> _pressedKeys = new();

        public void Bind(Key key, Action callback)
        {
            if (!_bindings.ContainsKey(key))
                _bindings[key] = callback;
        }

        public void Unbind(Key key)
        {
            if (_bindings.ContainsKey(key))
                _bindings.Remove(key);
        }

        public void CheckKeys()
        {
            foreach (var kvp in _bindings)
            {
                Key key = kvp.Key;
                Action action = kvp.Value;

                if (Keyboard.IsKeyDown(key))
                {
                    if (!_pressedKeys.Contains(key))
                    {
                        _pressedKeys.Add(key);
                        action?.Invoke();
                    }
                }
                else
                {
                    _pressedKeys.Remove(key);
                }
            }
        }

        public void ClearAll()
        {
            _bindings.Clear();
            _pressedKeys.Clear();
        }
    }
}
