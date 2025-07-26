// Neural v3.0 — OverlayController.cs
using System.Collections.Generic;
using SkiaSharp;

namespace JaysAi.Finale.Overlay
{
    public class OverlayController
    {
        private readonly List<IOverlayRenderer> _renderers;

        public bool IsActive { get; private set; } = true;

        public OverlayController()
        {
            _renderers = new List<IOverlayRenderer>();
        }

        public void AddRenderer(IOverlayRenderer renderer)
        {
            if (renderer != null && !_renderers.Contains(renderer))
            {
                _renderers.Add(renderer);
            }
        }

        public void RemoveRenderer(IOverlayRenderer renderer)
        {
            if (renderer != null && _renderers.Contains(renderer))
            {
                _renderers.Remove(renderer);
            }
        }

        public void ClearRenderers()
        {
            _renderers.Clear();
        }

        public void ToggleOverlay() => IsActive = !IsActive;

        public void EnableOverlay() => IsActive = true;

        public void DisableOverlay() => IsActive = false;

        public void RenderAll(SKCanvas canvas, int width, int height)
        {
            if (!IsActive || canvas == null) return;

            foreach (var renderer in _renderers)
            {
                if (renderer.IsActive)
                {
                    renderer.Draw(canvas, width, height);
                }
            }
        }
    }
}
