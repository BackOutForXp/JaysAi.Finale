//neural v3.0
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaysAi.Finale.Input
{
    public class SignalDelayCalibrator
    {
        private readonly List<long> _delaySamples = new();
        private readonly int _maxSamples;

        public SignalDelayCalibrator(int maxSamples = 50)
        {
            _maxSamples = maxSamples;
        }

        public void AddDelaySample(long delay)
        {
            if (_delaySamples.Count >= _maxSamples)
                _delaySamples.RemoveAt(0);
            _delaySamples.Add(delay);
        }

        public long GetAverageDelay()
        {
            return _delaySamples.Count > 0 ? (long)_delaySamples.Average() : 0;
        }

        public long GetMaxDelay()
        {
            return _delaySamples.Count > 0 ? _delaySamples.Max() : 0;
        }

        public long GetMinDelay()
        {
            return _delaySamples.Count > 0 ? _delaySamples.Min() : 0;
        }

        public void Reset()
        {
            _delaySamples.Clear();
        }
    }
}
