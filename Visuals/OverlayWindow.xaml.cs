// File: Visuals/OverlayWindow.xaml.cs
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Visuals.Helpers;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public partial class OverlayWindow : Window
    {
        private readonly ESPOverlay _espOverlay;
        private readonly CrosshairOverlay _crosshairOverlay;

        public OverlayWindow(ESPOverlay espOverlay, CrosshairOverlay crosshairOverlay)
        {
            InitializeComponent();
            _espOverlay = espOverlay;
            _crosshairOverlay = crosshairOverlay;

            Loaded += OnLoaded;
            CompositionTarget.Rendering += OnRender;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Make the window click-through and fullscreen
            var hwnd = new WindowInteropHelper(this).Handle;
            Win32Helper.MakeWindowTransparent(hwnd);
            Win32Helper.SetFullScreen(hwnd);
        }

        private void OnRender(object? sender, EventArgs e)
        {
            _espOverlay.Render(MainCanvas);
            _crosshairOverlay.Render(MainCanvas);
        }

        public void RefreshCrosshair()
        {
            _crosshairOverlay.Invalidate();
        }
    }
}
