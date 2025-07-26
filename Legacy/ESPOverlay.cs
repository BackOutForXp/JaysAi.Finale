// Neural v3.0 — EspOverlay.cs
using JaysAi.Finale.Data;
using JaysAi.Finale.Features;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Overlay;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Forms;

namespace JaysAi.Finale.Legacy
{
    public class EspOverlay : IDisposable
    {
        private readonly ESPModuleManager _espModule;
        private readonly EspDrawer _drawer;
        private readonly SKControl _skiaControl;
        private readonly Form _overlayForm;

        public bool IsActive { get; private set; }

        public EspOverlay(Form overlayForm, ESPModuleManager espModule)
        {
            _overlayForm = overlayForm;
            _espModule = espModule;
            _drawer = new EspDrawer(_espModule);

            _skiaControl = new SKControl();
            Initialize();
        }

        private void Initialize()
        {
            _skiaControl.Dock = DockStyle.Fill;
            _skiaControl.PaintSurface += OnPaintSurface;

            _overlayForm.Controls.Add(_skiaControl);
            IsActive = true;
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            int width = e.Info.Width;
            int height = e.Info.Height;

            _drawer.Draw(canvas, width, height);
        }

        public void Refresh() => _skiaControl.Invalidate();

        public void Dispose()
        {
            IsActive = false;
            _skiaControl.PaintSurface -= OnPaintSurface;
            _overlayForm.Controls.Remove(_skiaControl);
            _skiaControl.Dispose();
        }
    }
}
