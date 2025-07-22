//heavenly v3.0.0 – Frame Source Router for Capture & Processing
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.AI
{
    public static class CaptureEngine
    {
        private static IFrameSource _frameSource;
        public static bool Initialized => _frameSource != null;

        public static void Initialize(IFrameSource source)
        {
            _frameSource = source;
        }

        public static FrameSnapshot CaptureFrame()
        {
            if (!Initialized)
                throw new InvalidOperationException("CaptureEngine not initialized with a frame source.");

            return _frameSource.GetCurrentFrame();
        }

        public static void Shutdown()
        {
            _frameSource = null;
        }
    }
}
