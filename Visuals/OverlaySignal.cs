//monarch v2.1 – Overlay Render Dispatcher
using System.Collections.Concurrent;

namespace JaysAi.Finale.Visuals
{
    public static class OverlaySignal
    {
        private static readonly ConcurrentQueue<OverlayRectangle> _renderQueue = new();

        public static void Enqueue(OverlayRectangle rectangle)
        {
            _renderQueue.Enqueue(rectangle);
        }

        public static bool TryDequeue(out OverlayRectangle rectangle)
        {
            return _renderQueue.TryDequeue(out rectangle);
        }

        public static void Clear()
        {
            while (_renderQueue.TryDequeue(out _)) { }
        }

        public static int Count => _renderQueue.Count;
    }
}
