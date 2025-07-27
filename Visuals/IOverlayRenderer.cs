// Neural v3.1 — IOverlayRenderer.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public interface IOverlayRenderer
    {
        bool IsActive { get; set; }
        void Render(SKCanvas canvas, int width, int height);
    }
}
