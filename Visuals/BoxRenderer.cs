//monarch v2.1
using JaysAi.AI;
using JaysAi.Finale.AI;
using System;

namespace JaysAi.Finale.Visuals
{
    public class BoxRenderer
    {
        private readonly IOverlayContext overlay;

        public BoxRenderer(IOverlayContext overlayContext)
        {
            overlay = overlayContext ?? throw new ArgumentNullException(nameof(overlayContext));
        }

        public void DrawBox(BoundingBox box, ESPStyleConfig style)
        {
            overlay.DrawRectangle(
                x: box.X,
                y: box.Y,
                width: box.Width,
                height: box.Height,
                color: style.Color,
                thickness: style.Thickness
            );
        }
    }
}
