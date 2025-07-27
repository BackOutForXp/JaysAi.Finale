// Neural v3.1 — SkiaSurfaceFactory.cs
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public static class SkiaSurfaceFactory
    {
        public static SKSurface CreateSurface(IntPtr hwnd, int width, int height)
        {
            var info = new SKImageInfo(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);

            var surfaceProps = new GRSurfaceProps(GRSurfaceOrigin.BottomLeft);
            var backendRenderTarget = new GRBackendRenderTarget(
                width,
                height,
                0, // sample count
                0, // stencil bits
                new GRGlFramebufferInfo(0, SKColorType.Bgra8888.ToGlSizedFormat())
            );

            using var context = GRContext.CreateGl();
            return SKSurface.Create(context, backendRenderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Bgra8888);
        }

        public static SKSurface CreateSoftwareSurface(int width, int height)
        {
            var info = new SKImageInfo(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
            return SKSurface.Create(info);
        }
    }
}
