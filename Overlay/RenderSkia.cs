// File: Overlay/RenderSkia.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Features;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Visuals;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace JaysAi.Finale.Overlay
{
    public class RenderSkia : Form, IOverlayRenderer
    {
        private readonly SettingsManager<AppSettings> _settings;
        private readonly SKControl _skControl;

        private readonly Crosshair _crosshair;
        private readonly ESP _esp;
        private readonly IEnemyProvider _enemyProvider;

        public RenderSkia(SettingsManager<AppSettings> settings)
        {
            _settings = settings;
            _enemyProvider = new DummyEnemyProvider();
            _esp = new ESP(_settings, this, _enemyProvider);
            _crosshair = new Crosshair(_settings, this);

            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            ShowInTaskbar = false;
            BackColor = System.Drawing.Color.Black;
            TransparencyKey = System.Drawing.Color.Black;
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            StartPosition = FormStartPosition.Manual;
            Location = new System.Drawing.Point(0, 0);

            _skControl = new SKControl
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.Transparent
            };

            _skControl.PaintSurface += OnPaintSurface;
            Controls.Add(_skControl);
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            // Order matters — draw behind first
            _esp.Render(canvas, Width, Height);
            _crosshair.Render(canvas, Width, Height);
        }

        public void Redraw()
        {
            _skControl.Invalidate();
        }
    }
}
