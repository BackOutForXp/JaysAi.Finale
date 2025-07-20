//monarch v2.1
using System;
using JaysAi.SystemLogic;
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public class OverlaySignal
    {
        private readonly IOverlayContext overlay;
        private DateTime lastPulse = DateTime.MinValue;
        private bool visible = true;
        private readonly float x = 20f;
        private readonly float y = 20f;
        private readonly float size = 12f;
        private readonly Color pulseColor = Colors.LimeGreen;

        public OverlaySignal(IOverlayContext context)
        {
            overlay = context;
        }

        public void DrawStatus()
        {
            if ((DateTime.Now - lastPulse).TotalMilliseconds >= 500)
            {
                visible = !visible;
                lastPulse = DateTime.Now;
            }

            if (visible)
            {
                overlay.DrawCircle(
                    x: x,
                    y: y,
                    radius: size,
                    color: pulseColor,
                    thickness: 1.5f
                );
            }
        }
    }
}
