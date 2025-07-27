// Neural v3.1
using System;
using System.Diagnostics;

namespace JaysAi.Finale.Helpers
{
    public static class TimeUtils
    {
        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private static double _lastFrameTime;

        /// <summary>
        /// Call once per frame to update timing state.
        /// </summary>
        public static void TickFrame()
        {
            _lastFrameTime = _stopwatch.Elapsed.TotalSeconds;
        }

        /// <summary>
        /// Returns time (in seconds) since last TickFrame call.
        /// </summary>
        public static float DeltaTime
        {
            get
            {
                double now = _stopwatch.Elapsed.TotalSeconds;
                return (float)(now - _lastFrameTime);
            }
        }

        /// <summary>
        /// Returns total time since loader started.
        /// </summary>
        public static float TotalTime => (float)_stopwatch.Elapsed.TotalSeconds;
    }
}
