// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

namespace JaysAi.Finale.Utility
{
    public class PerformanceTracker
    {
        private readonly ConcurrentDictionary<string, Stopwatch> _timers;
        private readonly ConcurrentDictionary<string, long> _records;
        private readonly ConcurrentDictionary<string, long> _samples;

        public PerformanceTracker()
        {
            _timers = new ConcurrentDictionary<string, Stopwatch>();
            _records = new ConcurrentDictionary<string, long>();
            _samples = new ConcurrentDictionary<string, long>();
        }

        public void Start(string key)
        {
            var timer = _timers.GetOrAdd(key, _ => new Stopwatch());
            timer.Restart();
        }

        public void Stop(string key)
        {
            if (_timers.TryGetValue(key, out var timer))
            {
                timer.Stop();
                long elapsed = timer.ElapsedMilliseconds;

                _records.AddOrUpdate(key, elapsed, (_, old) => old + elapsed);
                _samples.AddOrUpdate(key, 1, (_, old) => old + 1);
            }
        }

        public double GetAverage(string key)
        {
            if (_records.TryGetValue(key, out var total) && _samples.TryGetValue(key, out var count) && count > 0)
            {
                return total / (double)count;
            }
            return 0;
        }

        public void Reset(string key)
        {
            _records.TryRemove(key, out _);
            _samples.TryRemove(key, out _);
            _timers.TryRemove(key, out _);
        }

        public void ResetAll()
        {
            _records.Clear();
            _samples.Clear();
            _timers.Clear();
        }

        public string GetReport()
        {
            return string.Join("\n", _records.Keys.Select(k => $"{k}: {GetAverage(k):F2} ms"));
        }
    }
}
