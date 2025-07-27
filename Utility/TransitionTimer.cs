// Neural v3.1 — TransitionTimer.cs
using System;

namespace JaysAi.Finale.Utility
{
    public class TransitionTimer
    {
        private DateTime _startTime;
        private TimeSpan _duration;
        private bool _active;

        public float Progress => _active ? (float)(DateTime.UtcNow - _startTime).TotalMilliseconds / (float)_duration.TotalMilliseconds : 0f;
        public bool IsComplete => _active && DateTime.UtcNow - _startTime >= _duration;

        public void Start(TimeSpan duration)
        {
            _duration = duration;
            _startTime = DateTime.UtcNow;
            _active = true;
        }

        public void Stop()
        {
            _active = false;
        }

        public void Reset()
        {
            _active = false;
            _startTime = DateTime.MinValue;
            _duration = TimeSpan.Zero;
        }
    }
}
