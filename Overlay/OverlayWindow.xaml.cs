// neural v3.0
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Overlay.Utils;
using JaysAi.Finale.Visuals;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace JaysAi.Finale.Overlay
{
    public partial class OverlayWindow : Window
    {
        private readonly DispatcherTimer _frameTimer;

        public OverlayWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            _frameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000.0 / 144) // target 144 FPS
            };
            _frameTimer.Tick += OnRenderFrame;
            _frameTimer.Start();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            Win32Helper.MakeWindowTransparent(hwnd);
            Win32Helper.SetOverlayStyles(hwnd);
        }

        private void OnRenderFrame(object? sender, EventArgs e)
        {
            OverlayCanvas.Children.Clear();
            OverlayRenderer.DrawAll(OverlayCanvas); // Delegates to OverlayRenderer
        }
    }
}
