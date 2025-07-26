// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JaysAi.Finale.Input
{
    public sealed class ControllerSignalBus
    {
        private static readonly Lazy<ControllerSignalBus> _instance = new(() => new ControllerSignalBus());
        public static ControllerSignalBus Instance => _instance.Value;

        private readonly ConcurrentDictionary<string, List<Action<object?>>> _subscribers;

        private ControllerSignalBus()
        {
            _subscribers = new ConcurrentDictionary<string, List<Action<object?>>>();
        }

        public void Subscribe(string signal, Action<object?> handler)
        {
            _subscribers.AddOrUpdate(
                signal,
                _ => new List<Action<object?>> { handler },
                (_, handlers) =>
                {
                    lock (handlers)
                    {
                        handlers.Add(handler);
                        return handlers;
                    }
                });
        }

        public void Unsubscribe(string signal, Action<object?> handler)
        {
            if (_subscribers.TryGetValue(signal, out var handlers))
            {
                lock (handlers)
                {
                    handlers.Remove(handler);
                    if (handlers.Count == 0)
                        _subscribers.TryRemove(signal, out _);
                }
            }
        }

        public void Publish(string signal, object? payload = null)
        {
            if (_subscribers.TryGetValue(signal, out var handlers))
            {
                List<Action<object?>> snapshot;
                lock (handlers)
                    snapshot = new List<Action<object?>>(handlers);

                foreach (var handler in snapshot)
                {
                    try
                    {
                        handler.Invoke(payload);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ControllerSignalBus] Error invoking handler for '{signal}': {ex.Message}");
                    }
                }
            }
        }

        public void ClearAll()
        {
            _subscribers.Clear();
        }
    }
}
