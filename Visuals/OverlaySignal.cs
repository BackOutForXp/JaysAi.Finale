// Neural v3.0 — OverlaySignal.cs
using System;

namespace JaysAi.Finale.Overlay
{
    public static class OverlaySignal
    {
        public static event Action<string>? OnOverlayEvent;

        /// <summary>
        /// Broadcasts a signal to all overlay listeners with a string event code.
        /// Example: "toggle_esp", "refresh_overlay", "hide_all"
        /// </summary>
        public static void Emit(string signalCode)
        {
            OnOverlayEvent?.Invoke(signalCode);
        }

        /// <summary>
        /// Allows overlay modules to subscribe to specific overlay events.
        /// </summary>
        public static void Subscribe(Action<string> handler)
        {
            OnOverlayEvent += handler;
        }

        /// <summary>
        /// Unsubscribes a handler from overlay signals.
        /// </summary>
        public static void Unsubscribe(Action<string> handler)
        {
            OnOverlayEvent -= handler;
        }

        /// <summary>
        /// Clears all listeners (useful during finalization).
        /// </summary>
        public static void ClearListeners()
        {
            OnOverlayEvent = null;
        }
    }
}
