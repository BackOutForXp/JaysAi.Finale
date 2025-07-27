// Neural v3.1 — RenderMode.cs
namespace JaysAi.Finale.Overlay
{
    public enum RenderMode
    {
        SkiaSharp = 0,
        D3DHook = 1,     // Reserved for future DirectX integration
        WpfCanvas = 2,   // Optional for WPF UI-layer rendering
    }
}
