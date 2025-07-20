//monarch v2.1
using System.Windows.Media;
using JaysAi.SystemLogic;

namespace JaysAi.Finale.Aim
{
    public class CompensationGraph
    {
        private readonly IOverlayContext overlay;

        public CompensationGraph(IOverlayContext overlayContext)
        {
            overlay = overlayContext;
        }

        public void DrawPattern(RecoilPattern pattern, float originX, float originY, Color dotColor)
        {
            if (pattern == null || pattern.Steps.Count == 0)
                return;

            float x = originX;
            float y = originY;

            foreach (var (dx, dy) in pattern.Steps)
            {
                x += dx * 5f; // Scaled for visual spacing
                y += dy * 5f;

                overlay.DrawCircle(
                    x: x,
                    y: y,
                    radius: 3f,
                    color: dotColor,
                    thickness: 1.0f
                );
            }
        }
    }
}
