//monarch v2.1
using System.Collections.Generic;
using JaysAi.AI;
using JaysAi.Finale.AI;
using JaysAi.Finale.Visuals;
using JaysAi.Visuals;

namespace JaysAi.Overlay
{
    public class AIOverlay
    {
        private readonly ESPModule esp;
        private readonly OverlayDrawer drawer;
        private readonly OverlaySignal signal;

        public bool Enabled { get; set; } = true;

        public AIOverlay(ESPModule espModule, OverlayDrawer overlayDrawer, OverlaySignal overlaySignal)
        {
            esp = espModule;
            drawer = overlayDrawer;
            signal = overlaySignal;
        }

        public void Render(List<PredictionResult> predictions)
        {
            if (!Enabled || predictions == null)
                return;

            esp.Draw(predictions);
            drawer.DrawFOVCircle();
            signal.DrawStatus();
        }
    }
}
