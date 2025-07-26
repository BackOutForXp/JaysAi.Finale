// Neural v3.0 — OverlayFinalizer.cs
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Overlay
{
    public static class OverlayFinalizer
    {
        private static readonly List<IDisposable> _disposables = new();

        /// <summary>
        /// Registers an overlay component for cleanup during finalization.
        /// </summary>
        public static void Register(IDisposable component)
        {
            if (component != null && !_disposables.Contains(component))
            {
                _disposables.Add(component);
            }
        }

        /// <summary>
        /// Clears and disposes all overlay components safely.
        /// </summary>
        public static void FinalizeAll()
        {
            foreach (var disposable in _disposables)
            {
                try
                {
                    disposable.Dispose();
                }
                catch (Exception ex)
                {
                    // Optional: log this to a debug system later
                    Console.WriteLine($"OverlayFinalizer error: {ex.Message}");
                }
            }

            _disposables.Clear();
        }

        /// <summary>
        /// Immediately dispose a single overlay component.
        /// </summary>
        public static void FinalizeComponent(IDisposable component)
        {
            if (component == null) return;

            try
            {
                component.Dispose();
                _disposables.Remove(component);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OverlayFinalizer single error: {ex.Message}");
            }
        }
    }
}
