//monarch v2.1
using JaysAi.SystemLogic;
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public class OverlayDrawer
    {
        private readonly IOverlayContext overlay;
        private readonly float screenCenterX;
        private readonly float screenCenterY;
        private readonly float fovRadius;
        private readonly Color fovColor;

        public OverlayDrawer(IOverlayContext overlayContext, float screenWidth, float screenHeight, float radius, Color color)
        {
            overlay = overlayContext;
            screenCenterX = screenWidth / 2f;
            screenCenterY = screenHeight / 2f;
            fovRadius = radius;
            fovColor = color;
        }

        public void DrawFOVCircle()
        {
            overlay.DrawCircle(
                x: screenCenterX,
                y: screenCenterY,
                radius: fovRadius,
                color: fovColor,
                thickness: 2.0f
            );
        }
    }
}
