// Neural v3.1 — RenderSkia.cs
using JaysAi.Finale.Overlay;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public class RenderSkia : UserControl
    {
        private readonly OverlayRenderer _overlayRenderer;
        private readonly SKElement _skElement;

        public RenderSkia(OverlayRenderer overlayRenderer)
        {
            _overlayRenderer = overlayRenderer;

            _skElement = new SKElement
            {
                IgnorePixelScaling = true
            };

            _skElement.PaintSurface += OnPaintSurface;
            Content = _skElement;

            Loaded += (_, _) => SetupOverlayLayer();
        }

        private void SetupOverlayLayer()
        {
            var hwndSource = (HwndSource)PresentationSource.FromVisual(this)!;
            hwndSource.CompositionTarget.BackgroundColor = Colors.Transparent;
            hwndSource.RootVisual.SetValue(Panel.ZIndexProperty, int.MaxValue);
        }

        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            int width = e.Info.Width;
            int height = e.Info.Height;

            _overlayRenderer.Render(canvas, width, height);
        }

        public void ForceRedraw()
        {
            _skElement.InvalidateVisual();
        }
    }
}
