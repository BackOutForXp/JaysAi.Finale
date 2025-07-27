// Neural v3.1
using System;

namespace JaysAi.Finale.Helpers
{
    public static class LatencyHelper
    {
        private static float _manualLatencyOverride = -1f;

        public static void SetManualOverride(float latencyMs)
        {
            _manualLatencyOverride = latencyMs;
        }

        public static void ClearOverride()
        {
            _manualLatencyOverride = -1f;
        }

        public static float GetCurrentLatencyMs()
        {
            if (_manualLatencyOverride >= 0f)
                return _manualLatencyOverride;

            // TODO: Replace with actual latency polling from game or network layer
            return 50f; // Default estimate
        }
    }
}
