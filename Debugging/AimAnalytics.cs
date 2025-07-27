using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Debugging
{
    public static class AimAnalytics
    {
        private static readonly List<double> _decisionLatencies = new();
        private static readonly List<double> _snapTimes = new();
        private static int _snapSuccessCount = 0;
        private static int _snapAttemptCount = 0;

        public static void RecordSnapAttempt(bool success)
        {
            _snapAttemptCount++;
            if (success) _snapSuccessCount++;
        }

        public static void RecordDecisionLatency(TimeSpan delta)
        {
            _decisionLatencies.Add(delta.TotalMilliseconds);
        }

        public static void RecordSnapTime(TimeSpan delta)
        {
            _snapTimes.Add(delta.TotalMilliseconds);
        }

        public static double GetAverageLatency()
        {
            if (_decisionLatencies.Count == 0) return 0;
            return Average(_decisionLatencies);
        }

        public static double GetAverageSnapTime()
        {
            if (_snapTimes.Count == 0) return 0;
            return Average(_snapTimes);
        }

        public static float GetSnapAccuracy()
        {
            if (_snapAttemptCount == 0) return 0f;
            return (float)_snapSuccessCount / _snapAttemptCount;
        }

        public static void Reset()
        {
            _decisionLatencies.Clear();
            _snapTimes.Clear();
            _snapSuccessCount = 0;
            _snapAttemptCount = 0;
        }

        private static double Average(List<double> values)
        {
            double total = 0;
            foreach (var v in values) total += v;
            return total / values.Count;
        }
    }
}
