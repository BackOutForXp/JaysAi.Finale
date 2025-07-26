// Neural v3.0 — OverlayBase.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public abstract class OverlayBase : IOverlayRenderer
    {
        /// <summary>
        /// Indicates if this overlay is currently active and should render.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Optional tag for debugging or identification.
        /// </summary>
        public virtual string Name => GetType().Name;

        /// <summary>
        /// Main rendering method called every frame.
        /// </summary>
        /// <param name="canvas">The SkiaSharp canvas to draw on.</param>
        /// <param name="screenWidth">Current screen width.</param>
        /// <param name="screenHeight">Current screen height.</param>
        public abstract void Draw(SKCanvas canvas, int screenWidth, int screenHeight);

        /// <summary>
        /// Toggles the active state.
        /// </summary>
        public void Toggle() => IsActive = !IsActive;

        /// <summary>
        /// Enables this overlay for drawing.
        /// </summary>
        public void Enable() => IsActive = true;

        /// <summary>
        /// Disables this overlay from drawing.
        /// </summary>
        public void Disable() => IsActive = false;
    }
}
