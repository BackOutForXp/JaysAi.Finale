// Neural v3.0 — FrameRateCalculator.cs
using System;
using System.Diagnostics;

namespace JaysAi.Finale.Helpers
{
    public class FrameRateCalculator
    {
        private int _frameCount;
        private double _elapsedTime;
        private readonly Stopwatch _stopwatch;

        public double CurrentFps { get; private set; }

        public FrameRateCalculator()
        {
            _frameCount = 0;
            _elapsedTime = 0;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void FrameTick()
        {
            _frameCount++;
            _elapsedTime += _stopwatch.Elapsed.TotalSeconds;
            _stopwatch.Restart();

            if (_elapsedTime >= 1.0)
            {
                CurrentFps = _frameCount / _elapsedTime;
                _frameCount = 0;
                _elapsedTime = 0;
            }
        }

        public void Reset()
        {
            _frameCount = 0;
            _elapsedTime = 0;
            _stopwatch.Restart();
            CurrentFps = 0;
        }
    }
}
