// Neural v3.1 — OverlayRenderer.cs
using JaysAi.Finale.Settings;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace JaysAi.Finale.Overlay
{
    public class OverlayRenderer
    {
        private readonly List<IOverlayRenderer> _renderers = new();
        private readonly SKElement _skElement;
        private readonly DispatcherTimer _renderTimer;

        public OverlayRenderer(SKElement skElement)
        {
            _skElement = skElement;
            _skElement.PaintSurface += OnPaintSurface;

            _renderTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000 / UserSettings.Instance.Get("OverlayFPS", 60))
            };
            _renderTimer.Tick += (s, e) => _skElement.InvalidateVisual();
        }

        public void Start()
        {
            _renderTimer.Start();
        }

        public void Stop()
        {
            _renderTimer.Stop();
        }

        public void RegisterRenderer(IOverlayRenderer renderer)
        {
            if (!_renderers.Contains(renderer))
                _renderers.Add(renderer);
        }

        public void UnregisterRenderer(IOverlayRenderer renderer)
        {
            _renderers.Remove(renderer);
        }

        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            int width = e.Info.Width;
            int height = e.Info.Height;

            foreach (var renderer in _renderers)
            {
                if (renderer.IsActive)
                    renderer.Render(canvas, width, height);
            }
        }
    }
}
