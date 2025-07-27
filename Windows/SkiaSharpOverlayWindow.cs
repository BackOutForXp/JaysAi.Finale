// Neural v3.1
using JaysAi.Finale.Overlay;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace JaysAi.Finale.Windows
{
    public partial class SkiaSharpOverlayWindow : Window
    {
        private readonly OverlayRenderer _overlayRenderer = new();
        private readonly SKElement _skiaElement;

        public SkiaSharpOverlayWindow()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            Background = null!;
            IsHitTestVisible = false;
            Topmost = true;
            ResizeMode = ResizeMode.NoResize;
            ShowInTaskbar = false;

            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            Left = 0;
            Top = 0;

            _skiaElement = new SKElement();
            _skiaElement.PaintSurface += OnPaintSurface;

            Content = _skiaElement;

            Loaded += (_, _) => StartRenderLoop();
        }

        private void StartRenderLoop()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16.6) // ~60 FPS
            };

            timer.Tick += (_, _) => _skiaElement.InvalidateVisual();
            timer.Start();
        }

        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var width = e.Info.Width;
            var height = e.Info.Height;

            canvas.Clear(SKColors.Transparent);

            _overlayRenderer.Render(canvas, width, height);
        }

        public OverlayRenderer GetRenderer() => _overlayRenderer;
    }
}
