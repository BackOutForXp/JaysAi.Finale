// Neural v3.0 — NeuralTelemetryBus.cs
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.SystemLogic.Telemetry
{
    public static class NeuralTelemetryBus
    {
        private static readonly List<Action<NeuralFeedbackState>> Subscribers = new();

        public static void Broadcast(NeuralFeedbackState state)
        {
            foreach (var subscriber in Subscribers)
                subscriber.Invoke(state);
        }

        public static void Subscribe(Action<NeuralFeedbackState> handler)
        {
            if (!Subscribers.Contains(handler))
                Subscribers.Add(handler);
        }

        public static void Unsubscribe(Action<NeuralFeedbackState> handler)
        {
            Subscribers.Remove(handler);
        }
    }
}
