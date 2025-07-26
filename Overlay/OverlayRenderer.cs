// Neural v3.0 — OverlayRenderer.cs
using System;
using System.Collections.Generic;
using JaysAi.Finale.Visuals;          // for IOverlayRenderer, VisualEsp, etc.
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    /// <summary>
    ///  Central rendering coordinator for all overlay layers (ESP, FOV rings, crosshair, etc.).
    /// </summary>
    public sealed class OverlayRenderer : IDisposable
    {
        private readonly List<IOverlayRenderer> _renderers = new();
        private SKSurface? _surface;          // off-screen target
        private SKCanvas? _canvas;           // cached canvas
        private bool _isDisposed;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        public OverlayRenderer(int width, int height)
        {
            Resize(width, height);

            // --- Register default layers here ---
            _renderers.Add(new VisualEsp());
            // You can add Crosshair, FOV rings, debug layers, etc. later
        }

        /// <summary>Call once per video-frame.</summary>
        public void Render()
        {
            if (_surface == null || _canvas == null) return;

            _canvas.Clear(OverlayColor.Transparent);

            foreach (var r in _renderers)
            {
                if (r.IsActive)
                    r.Draw(_canvas, ScreenWidth, ScreenHeight);
            }

            _canvas.Flush();   // push drawing commands
        }

        /// <summary>
        ///  Resize back-buffer when the window or monitor resolution changes.
        /// </summary>
        public void Resize(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            _surface?.Dispose();
            _surface = SKSurface.Create(new SKImageInfo(width, height, SKColorType.Bgra8888));
            _canvas = _surface.Canvas;
        }

        /// <summary>Add or remove overlay layers at runtime.</summary>
        public void RegisterRenderer(IOverlayRenderer renderer) => _renderers.Add(renderer);
        public void UnregisterRenderer(IOverlayRenderer renderer) => _renderers.Remove(renderer);

        public SKImage Snapshot() => _surface!.Snapshot();

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;

            _canvas?.Dispose();
            _surface?.Dispose();
        }
    }
}
