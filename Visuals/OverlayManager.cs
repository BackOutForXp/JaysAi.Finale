//monarch v2.1
using System;

namespace JaysAi.Finale.Visuals
{
    public class OverlayManager
    {
        private readonly ESPDrawer espDrawer;
        private bool isEnabled;
        private int drawDelayMs = 16;

        public OverlayManager(ESPDrawer drawer)
        {
            espDrawer = drawer;
        }

        public void Enable() => isEnabled = true;
        public void Disable() => isEnabled = false;

        public void Toggle() => isEnabled = !isEnabled;
        public bool IsEnabled() => isEnabled;

        public void SetDelay(int ms) => drawDelayMs = Math.Clamp(ms, 5, 1000);

        public void RenderFrame()
        {
            if (!isEnabled) return;
            espDrawer.DrawAll();
            System.Threading.Thread.Sleep(drawDelayMs);
        }
    }
}
