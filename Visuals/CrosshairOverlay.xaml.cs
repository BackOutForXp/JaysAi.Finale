// File: Visuals/CrosshairOverlay.cs
using JaysAi.Finale.Settings;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace JaysAi.Finale.Visuals
{
    public class CrosshairOverlay : Window
    {
        private readonly SettingsManager<AppSettings> _settingsManager;
        private readonly DispatcherTimer _drawTimer;
        private readonly SKElement _skElement;

        public bool IsEnabled { get; set; } = true;

        public CrosshairOverlay(SettingsManager<AppSettings> settingsManager)
        {
            _settingsManager = settingsManager;

            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            Top = 0;
            Left = 0;
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            Background = null;
            Topmost = true;
            ShowInTaskbar = false;
            ResizeMode = ResizeMode.NoResize;

            _skElement = new SKElement();
            _skElement.PaintSurface += OnPaintSurface;
            Content = _skElement;

            _drawTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            _drawTimer.Tick += (s, e) => _skElement.InvalidateVisual();

            Loaded += (_, _) => MakeClickthrough();
        }

        public void UpdateSettings(AppSettings settings)
        {
            // Called when user updates the crosshair settings in UI
            _skElement.InvalidateVisual();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            MakeClickthrough();
        }

        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            if (!IsEnabled) return;

            var settings = _settingsManager.Settings;
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            float centerX = e.Info.Width / 2f;
            float centerY = e.Info.Height / 2f;
            float length = settings.CrosshairLength;
            float thickness = settings.CrosshairThickness;
            var color = SKColor.Parse(settings.CrosshairColorHex);

            using var paint = new SKPaint
            {
                Color = color,
                IsAntialias = true,
                StrokeWidth = thickness,
                Style = SKPaintStyle.Stroke
            };

            // Horizontal line
            canvas.DrawLine(centerX - length, centerY, centerX + length, centerY, paint);
            // Vertical line
            canvas.DrawLine(centerX, centerY - length, centerX, centerY + length, paint);

            if (settings.ShowCenterDot)
            {
                canvas.DrawCircle(centerX, centerY, thickness * 1.5f, paint);
            }
        }

        public new void Show()
        {
            base.Show();
            _drawTimer.Start();
        }

        public new void Hide()
        {
            _drawTimer.Stop();
            base.Hide();
        }

        private void MakeClickthrough()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int extendedStyle = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE);
            NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE, extendedStyle | NativeMethods.WS_EX_TRANSPARENT | NativeMethods.WS_EX_LAYERED);
        }
    }
}
