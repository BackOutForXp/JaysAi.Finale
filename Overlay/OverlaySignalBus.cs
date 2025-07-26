// neural v3.0
using System;
using System.Collections.Concurrent;

namespace JaysAi.Finale.Overlay
{
    public static class OverlaySignalBus
    {
        private static readonly ConcurrentDictionary<string, Action<object>> _signalHandlers = new();

        public static void Register(string signalName, Action<object> handler)
        {
            _signalHandlers[signalName] = handler;
        }

        public static void Unregister(string signalName)
        {
            _signalHandlers.TryRemove(signalName, out _);
        }

        public static void Emit(string signalName, object payload)
        {
            if (_signalHandlers.TryGetValue(signalName, out var handler))
            {
                try
                {
                    handler(payload);
                }
                catch (Exception ex)
                {
                    // Optional: log or suppress safely
                    Console.WriteLine($"[OverlaySignalBus] Error handling signal '{signalName}': {ex.Message}");
                }
            }
        }

        public static void Clear()
        {
            _signalHandlers.Clear();
        }
    }
}
