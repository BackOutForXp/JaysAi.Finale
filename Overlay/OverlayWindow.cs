using JaysAi.Finale.AI;
using JaysAi.Finale.Features;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace JaysAi.Finale.Overlay
{
    public class OverlayWindow : Window
    {
        private readonly AppSettings _settings;
        private readonly SKElement _skElement;
        private readonly SkiaRenderer _renderer;
        private DispatcherTimer _renderTimer;

        public OverlayWindow(AppSettings settings)
        {
            _settings = settings;
            _renderer = new SkiaRenderer(_settings);

            Title = "JaysAi Overlay";
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            Top = 0;
            Left = 0;
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            Background = null;
            Topmost = true;
            ShowInTaskbar = false;

            _skElement = new SKElement();
            _skElement.PaintSurface += OnPaintSurface;

            Content = _skElement;

            Loaded += (_, _) => MakeClickThrough();
            StartRendering();
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            var visibleEnemies = EnemyScanner.LastVisible;
            var target = AimAssist.LastTarget;

            _renderer.Render(canvas, visibleEnemies, target);
        }

        private void StartRendering()
        {
            _renderTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000f / _settings.OverlayFPS)
            };
            _renderTimer.Tick += (_, _) => _skElement.InvalidateVisual();
            _renderTimer.Start();
        }

        private void MakeClickThrough()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int exStyle = NativeMethods.GetWindowLong(hwnd, -20);
            NativeMethods.SetWindowLong(hwnd, -20, exStyle | 0x80000 | 0x20); // WS_EX_LAYERED | WS_EX_TRANSPARENT
        }
    }
}
