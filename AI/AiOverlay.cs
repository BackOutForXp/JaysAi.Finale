//heavenly v3.0 – Overlay Queue Handler
using JaysAi.Finale.Visuals;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public static class AiOverlay
    {
        private static readonly List<OverlayRectangle> _queuedOverlays = new();

        public static void QueueRectangle(float x, float y, float width, float height, string label, OverlayColor color)
        {
            _queuedOverlays.Add(new OverlayRectangle
            {
                X = x,
                Y = y,
                Width = width,
                Height = height,
                Label = label,
                Color = color
            });
        }

        public static void QueueCrosshair(float x, float y, float radius, OverlayColor color)
        {
            _queuedOverlays.Add(new OverlayRectangle
            {
                X = x - radius,
                Y = y - radius,
                Width = radius * 2,
                Height = radius * 2,
                Label = "LOCK",
                Color = color,
                IsCrosshair = true
            });
        }

        public static List<OverlayRectangle> GetAndFlushQueue()
        {
            var copy = new List<OverlayRectangle>(_queuedOverlays);
            _queuedOverlays.Clear();
            return copy;
        }

        public static bool HasPendingOverlays => _queuedOverlays.Count > 0;
    }
}
