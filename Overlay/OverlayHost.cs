// Neural v3.0 — OverlayHost.cs
using JaysAi.Finale.Helpers;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace JaysAi.Finale.Overlay
{
    public class OverlayHost : Window
    {
        public OverlayHost()
        {
            InitializeOverlayWindow();
        }

        private void InitializeOverlayWindow()
        {
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            Top = 0;
            Left = 0;

            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            Background = Brushes.Transparent;
            Topmost = true;
            ShowInTaskbar = false;
            ResizeMode = ResizeMode.NoResize;

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;

            // Enable click-through and transparency
            int extendedStyle = Win32.GetWindowLong(hwnd, Win32.GWL_EXSTYLE);
            Win32.SetWindowLong(hwnd, Win32.GWL_EXSTYLE, extendedStyle | Win32.WS_EX_TRANSPARENT | Win32.WS_EX_TOOLWINDOW);
        }
    }
}
