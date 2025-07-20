// Monarch v1.0 – IOverlayRenderer.cs
// ✅ Monarch Fix Checklist
// [x] Interface for ESP rendering
// [x] Supports future SkiaSharp/SharpDX implementations
// [x] Abstracts away drawing details

using System.Drawing;

namespace JaysAi.Finale.Visuals
{
    public interface IOverlayRenderer
    {
        void DrawBox(Rectangle rect, Color color, float thickness);
        void DrawText(string text, Point position, Color color, float fontSize);
        void Clear();
        void Present();
    }
}
