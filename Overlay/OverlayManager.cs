// neural v3.0
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace JaysAi.Finale.Overlay
{
    public sealed class OverlayManager
    {
        private static readonly Lazy<OverlayManager> _instance = new(() => new OverlayManager());
        public static OverlayManager Instance => _instance.Value;

        private Window? _overlayWindow;
        private OverlayRenderCoordinator? _renderCoordinator;
        private DispatcherTimer? _renderTimer;

        private OverlayManager() { }

        public void Initialize(SKElement canvas)
        {
            _renderCoordinator = new OverlayRenderCoordinator(canvas);
            StartRenderLoop();
        }

        public void AttachOverlayWindow(Window window)
        {
            _overlayWindow = window;
            MakeOverlayClickThrough(window);
        }

        private void StartRenderLoop()
        {
            _renderTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000 / 60) // 60 FPS
            };
            _renderTimer.Tick += (_, _) => _renderCoordinator?.DrawFrame();
            _renderTimer.Start();
        }

        private void MakeOverlayClickThrough(Window window)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            exStyle |= WS_EX_TRANSPARENT | WS_EX_LAYERED;
            SetWindowLong(hwnd, GWL_EXSTYLE, exStyle);
        }

        #region WinAPI

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_LAYERED = 0x00080000;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int value);

        #endregion
    }
}
