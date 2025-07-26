// Neural v3.1 — OverlayRenderer.cs
using System;
using System.Collections.Generic;
using JaysAi.Finale.Visuals;
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    /// <summary>
    /// Central rendering coordinator for all overlay layers (ESP, FOV rings, crosshair, etc.).
    /// </summary>
    public sealed class OverlayRenderer : IDisposable
    {
        private readonly List<IOverlayRenderer> _renderers = new();
        private SKSurface? _surface;
        private SKCanvas? _canvas;
        private bool _isDisposed;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        public OverlayRenderer(int width, int height)
        {
            Resize(width, height);

            // --- Register default layers ---
            _renderers.Add(new VisualEsp());
            _renderers.Add(new FovOverlayRenderer());  // Added FOV circle
            // Add CrosshairRenderer, AimTraceRenderer, etc. as needed
        }

        public void Render()
        {
            if (_surface == null || _canvas == null) return;

            _canvas.Clear(OverlayColor.Transparent); // fully transparent background

            foreach (var r in _renderers)
            {
                if (r.IsActive)
                    r.Draw(_canvas, ScreenWidth, ScreenHeight);
            }

            _canvas.Flush(); // push drawing commands to frame
        }

        public void Resize(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            var info = new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

            _surface = SKSurface.Create(info);
            _canvas = _surface?.Canvas;
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _surface?.Dispose();
            _canvas?.Dispose();
            _isDisposed = true;
        }
    }
}
