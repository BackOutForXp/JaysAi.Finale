//monarch v2.1
using JaysAi.AI.Models;
using JaysAi.Finale.AI;
using JaysAi.Finale.Visuals;

namespace JaysAi.Overlay
{
    public class OverlayFinalizer
    {
        private readonly IRenderBackend backend;
        private readonly OverlayDrawer drawer;
        private readonly TargetTracker tracker;
        private readonly AimPathPredictor predictor;
        private readonly DrawConfig config;

        public OverlayFinalizer(IRenderBackend backend,
                                OverlayDrawer drawer,
                                TargetTracker tracker,
                                AimPathPredictor predictor,
                                DrawConfig config)
        {
            this.backend = backend;
            this.drawer = drawer;
            this.tracker = tracker;
            this.predictor = predictor;
            this.config = config;
        }

        public void DrawFrame()
        {
            backend.Clear();

            if (!tracker.HasTargets())
                return;

            foreach (TargetData target in tracker.GetAllTargets())
            {
                drawer.DrawTarget(target, config);
            }

            var current = tracker.GetCurrentTarget();
            if (current != null)
            {
                predictor.DrawPredictionLine(current);
            }

            backend.Present();
        }
    }
}
