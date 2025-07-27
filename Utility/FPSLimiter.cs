using System;
using System.Diagnostics;
using System.Threading;

namespace JaysAi.Finale.Utility
{
    public class FPSLimiter
    {
        private readonly int _targetFPS;
        private readonly Stopwatch _stopwatch = new();

        public FPSLimiter(int targetFPS = 60)
        {
            _targetFPS = Math.Clamp(targetFPS, 15, 240);
        }

        public void Wait()
        {
            if (!_stopwatch.IsRunning)
                _stopwatch.Start();

            long frameTicks = 1000 / _targetFPS;
            long elapsed = _stopwatch.ElapsedMilliseconds;

            if (elapsed < frameTicks)
                Thread.Sleep((int)(frameTicks - elapsed));

            _stopwatch.Restart();
        }
    }
}
