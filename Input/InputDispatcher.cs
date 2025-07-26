// neural v3.0
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Input
{
    public class InputDispatcher
    {
        private readonly Dictionary<string, Action<object>> _handlers = new();

        public void Register(string key, Action<object> handler)
        {
            if (!_handlers.ContainsKey(key))
                _handlers[key] = handler;
        }

        public void Unregister(string key)
        {
            if (_handlers.ContainsKey(key))
                _handlers.Remove(key);
        }

        public void Dispatch(string key, object payload)
        {
            if (_handlers.TryGetValue(key, out var handler))
                handler.Invoke(payload);
        }

        public void Clear()
        {
            _handlers.Clear();
        }

        public bool Contains(string key) => _handlers.ContainsKey(key);
    }
}
