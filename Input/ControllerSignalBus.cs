//heavenly v3.0 – Input Signal Broadcaster
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Input
{
    public static class ControllerSignalBus
    {
        private static readonly Dictionary<string, List<Action>> signalListeners = new();
        private static readonly Dictionary<string, List<Action<object?>>> payloadListeners = new();

        public static void Subscribe(string signal, Action listener)
        {
            if (!signalListeners.ContainsKey(signal))
                signalListeners[signal] = new List<Action>();

            signalListeners[signal].Add(listener);
        }

        public static void Subscribe<T>(string signal, Action<T?> listener)
        {
            if (!payloadListeners.ContainsKey(signal))
                payloadListeners[signal] = new List<Action<object?>>();

            payloadListeners[signal].Add(payload => listener((T?)payload));
        }

        public static void Unsubscribe(string signal, Action listener)
        {
            if (signalListeners.ContainsKey(signal))
                signalListeners[signal].Remove(listener);
        }

        public static void Unsubscribe<T>(string signal, Action<T?> listener)
        {
            if (payloadListeners.ContainsKey(signal))
                payloadListeners[signal].RemoveAll(l => l.Equals(listener));
        }

        public static void Emit(string signal)
        {
            if (signalListeners.ContainsKey(signal))
            {
                foreach (var listener in signalListeners[signal])
                {
                    listener?.Invoke();
                }
            }
        }

        public static void Emit<T>(string signal, T payload)
        {
            if (payloadListeners.ContainsKey(signal))
            {
                foreach (var listener in payloadListeners[signal])
                {
                    listener?.Invoke(payload);
                }
            }
        }

        public static void Clear()
        {
            signalListeners.Clear();
            payloadListeners.Clear();
        }
    }
}
