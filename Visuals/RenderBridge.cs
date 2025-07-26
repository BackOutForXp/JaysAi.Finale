// Heavenly-tier v3.0
using System;
using System.Collections.Generic;
using System.Threading;
using SkiaSharp;

namespace JaysAi.Finale.Visuals
{
    public static class RenderBridge
    {
        private static readonly object _sync = new();
        private static readonly List<IRenderable> _renderables = new();

        public static void Register(IRenderable renderable)
        {
            lock (_sync)
            {
                if (!_renderables.Contains(renderable))
                    _renderables.Add(renderable);
            }
        }

        public static void Unregister(IRenderable renderable)
        {
            lock (_sync)
            {
                _renderables.Remove(renderable);
            }
        }

        public static void RenderAll(SKCanvas canvas, float centerX, float centerY)
        {
            lock (_sync)
            {
                foreach (var renderable in _renderables)
                {
                    try
                    {
                        renderable?.Render(canvas, centerX, centerY);
                    }
                    catch (Exception ex)
                    {
                        // Optional: log or handle failure
                        System.Diagnostics.Debug.WriteLine($"Render error: {ex.Message}");
                    }
                }
            }
        }

        public static void Clear()
        {
            lock (_sync)
            {
                _renderables.Clear();
            }
        }
    }

    public interface IRenderable
    {
        void Render(SKCanvas canvas, float centerX, float centerY);
    }
}
