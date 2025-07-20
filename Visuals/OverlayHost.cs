// Monarch v1.0 – OverlayHost.cs
// ✅ Monarch Fix Checklist
// [x] Creates transparent topmost window
// [x] Runs render loop
// [x] Calls OverlayDrawer.Draw()

using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace JaysAi.Finale.Visuals
{
    public class OverlayHost : Form
    {
        private readonly OverlayDrawer _drawer;
        private readonly Thread _renderThread;

        public OverlayHost()
        {
            _drawer = new OverlayDrawer();
            _renderThread = new Thread(RenderLoop)
            {
                IsBackground = true
            };

            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            BackColor = System.Drawing.Color.Lime;
            TransparencyKey = System.Drawing.Color.Lime;
            ShowInTaskbar = false;
            WindowState = FormWindowState.Maximized;

            _renderThread.Start();
        }

        private void RenderLoop()
        {
            using var window = new SKGLControl();
            Controls.Add(window);

            while (true)
            {
                window.Invalidate();
                Thread.Sleep(16); // ~60 FPS
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var g = e.Graphics;
            using var bitmap = new SKBitmap(Width, Height);
            using var canvas = new SKCanvas(bitmap);

            _drawer.Draw(canvas, Width, Height);

            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode();
            g.DrawImage(System.Drawing.Image.FromStream(data.AsStream()), 0, 0);
        }
    }
}
