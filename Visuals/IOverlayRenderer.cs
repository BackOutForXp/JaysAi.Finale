// Neural v3.0 — IOverlayRenderer.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public interface IOverlayRenderer
    {
        /// <summary>
        /// Whether this renderer should be active and visible.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Called every frame to render overlay graphics.
        /// </summary>
        /// <param name="canvas">The SkiaSharp canvas to draw on.</param>
        /// <param name="screenWidth">The width of the screen or render area.</param>
        /// <param name="screenHeight">The height of the screen or render area.</param>
        void Draw(SKCanvas canvas, int screenWidth, int screenHeight);
    }
}
