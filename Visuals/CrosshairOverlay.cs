// Heavenly-tier v3.0
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace JaysAi.Finale.Visuals
{
    public class CrosshairOverlay : Form
    {
        private SKControl skControl;
        private Thread renderThread;
        private bool running = true;

        public CrosshairOverlay()
        {
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            ShowInTaskbar = false;
            BackColor = System.Drawing.Color.Black;
            TransparencyKey = System.Drawing.Color.Black;
            Width = ScreenUtils.Width;
            Height = ScreenUtils.Height;
            StartPosition = FormStartPosition.Manual;
            Location = new System.Drawing.Point(0, 0);

            skControl = new SKControl
            {
                Dock = DockStyle.Fill
            };
            skControl.PaintSurface += OnPaintSurface;

            Controls.Add(skControl);

            renderThread = new Thread(RenderLoop)
            {
                IsBackground = true
            };
            renderThread.Start();
        }

        private void RenderLoop()
        {
            while (running)
            {
                skControl.Invalidate();
                Thread.Sleep(16); // ~60fps
            }
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            if (UserSettingsProvider.Current.EnableCrosshair)
            {
                CrosshairDrawer.Draw(canvas,
                    UserSettingsProvider.Current.CrosshairType,
                    UserSettingsProvider.Current.CrosshairSize,
                    SKColors.Red);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            running = false;
            renderThread?.Join();
            base.OnFormClosing(e);
        }
    }
}
