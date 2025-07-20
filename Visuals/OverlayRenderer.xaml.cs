// File: Visuals/OverlayRenderer.xaml.cs
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace JaysAi.Finale.Visuals
{
    public partial class OverlayRenderer : UserControl
    {
        private readonly AppSettings _settings;
        private List<Enemy> _enemies = new();
        private Vector2 _playerPosition = Vector2.Zero;

        public OverlayRenderer(AppSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        public void UpdateData(Vector2 playerPosition, List<Enemy> enemies)
        {
            _playerPosition = playerPosition;
            _enemies = enemies;
            InvalidateVisual(); // Triggers redraw
        }

        private void SKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var info = e.Info;
            canvas.Clear(SKColors.Transparent);

            if (_settings.EnableESP)
                ESPDrawer.DrawEnemies(canvas, _enemies, _settings);

            if (_settings.EnableFovRing)
                FovRingRenderer.DrawFov(canvas, _playerPosition, _settings);

            if (_settings.EnableCrosshair)
                CrosshairDrawer.DrawCrosshair(canvas, info.Width, info.Height, _settings);
        }
    }
}
