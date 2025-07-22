//monarch v2.1.11 – Central AI Execution Dispatcher
using JaysAi.Finale.Visuals;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.AI
{
    public static class AiManager
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;

            PredictionEngine.Initialize();
            AiMemory.Initialize(); // Ensures persistent memory tracking
            OverlaySignal.Initialize(); // Prepares signaling bridge
            _initialized = true;
        }

        public static void Update()
        {
            if (!_initialized)
                Initialize();

            var detectedObjects = YoloDetector.GetDetectedObjects();
            AiOverlay.ProcessVisuals(detectedObjects);
            var bestTarget = TargetSelector.GetBestTarget(detectedObjects);

            if (bestTarget != null)
            {
                SnapAssist.LockOn(bestTarget);
            }
        }
    }
}
