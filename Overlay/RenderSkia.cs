// Neural v3.0 — RenderSkia.cs
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Windows.Controls;
using System.Windows.Forms;

namespace JaysAi.Finale.Overlay
{
    public class RenderSkia : IDisposable
    {
        private readonly Form _form;
        private readonly SKControl _skiaControl;
        private readonly CrosshairRenderer _crosshairRenderer;

        public bool IsRunning { get; private set; }

        public RenderSkia(Form form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form));
            _skiaControl = new SKControl();
            _crosshairRenderer = new CrosshairRenderer();

            Initialize();
        }

        private void Initialize()
        {
            _skiaControl.Dock = DockStyle.Fill;
            _skiaControl.PaintSurface += OnPaintSurface;
            _form.Controls.Add(_skiaControl);
            _form.FormClosing += (_, _) => Dispose();

            IsRunning = true;
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            int width = e.Info.Width;
            int height = e.Info.Height;

            _crosshairRenderer.Render(canvas, width, height);
        }

        public void ToggleCrosshair() => _crosshairRenderer.Toggle();

        public void SetCrosshairColor(SKColor color) => _crosshairRenderer.SetColor(color);

        public void SetCrosshairSize(float size) => _crosshairRenderer.SetSize(size);

        public void SetCrosshairThickness(float thickness) => _crosshairRenderer.SetThickness(thickness);

        public void SetCrosshairStyle(CrosshairStyle style) => _crosshairRenderer.SetStyle(style);

        public void Refresh() => _skiaControl.Invalidate();

        public void Dispose()
        {
            IsRunning = false;
            _skiaControl.PaintSurface -= OnPaintSurface;
            _form?.Controls.Remove(_skiaControl);
            _skiaControl?.Dispose();
        }
    }
}
