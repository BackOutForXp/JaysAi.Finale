// neural v3.0
using JaysAi.Finale.Overlay;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Visuals;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace JaysAi.Finale.Overlay
{
    public partial class AIOverlay : Window
    {
        private readonly DispatcherTimer _renderTimer;
        private readonly OverlayRenderCoordinator _renderCoordinator;

        public AIOverlay()
        {
            InitializeComponent();
            _renderCoordinator = new OverlayRenderCoordinator(OverlayCanvas);
            Loaded += OnLoaded;

            _renderTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000 / 144) // match your capture FPS
            };
            _renderTimer.Tick += (s, e) => _renderCoordinator.DrawFrame();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            MakeWindowTransparent();
            _renderTimer.Start();
        }

        private void MakeWindowTransparent()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int extendedStyle = Win32.GetWindowLong(hwnd, Win32.GWL_EXSTYLE);
            Win32.SetWindowLong(hwnd, Win32.GWL_EXSTYLE, extendedStyle | Win32.WS_EX_TRANSPARENT | Win32.WS_EX_LAYERED);
        }

        public void ShutdownOverlay()
        {
            _renderTimer.Stop();
            Close();
        }
    }
}
