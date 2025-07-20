//monarch v2.1 – Central AI Execution Dispatcher
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
            _initialized = true;
        }

        public static void Update()
        {
            if (!_initialized)
                Initialize();

            // Step 1: Gather detections
            var detectedObjects = YoloDetector.GetDetectedObjects();

            // Step 2: Visual overlay queue
            foreach (var obj in detectedObjects)
            {
                if (obj.IsEnemy)
                {
                    AiOverlay.QueueRectangle(obj.X, obj.Y, obj.Width, obj.Height, "ENEMY", OverlayColor.Red);
                }
            }

            // Step 3: Target selection and aim logic
            var bestTarget = TargetSelector.GetBestTarget(detectedObjects);

            if (bestTarget != null)
            {
                SnapAssist.LockOn(bestTarget);
            }

            // Step 4: Final rendering output is handled by OverlayDrawer
        }
    }
}
