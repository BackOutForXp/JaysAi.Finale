// Neural v3.1 — OverlayWindow.xaml.cs
using System;
using System.Windows;
using System.Windows.Threading;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Windows
{
    public partial class OverlayWindow : Window
    {
        private readonly DispatcherTimer _renderTimer;
        private readonly OverlayRenderer _overlayRenderer;

        public OverlayWindow(OverlayRenderer overlayRenderer)
        {
            InitializeComponent();

            _overlayRenderer = overlayRenderer;
            SkiaOverlay.Content = new RenderSkia(_overlayRenderer);

            _renderTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) // ~60 FPS
            };
            _renderTimer.Tick += (_, _) => Redraw();
            _renderTimer.Start();
        }

        private void Redraw()
        {
            if (SkiaOverlay.Content is RenderSkia renderSkia)
            {
                renderSkia.ForceRedraw();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _renderTimer.Stop();
            base.OnClosed(e);
        }
    }
}
