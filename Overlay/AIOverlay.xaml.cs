//neural v3.0

using JaysAi.Finale.AI;
using JaysAi.Finale.Core;
using JaysAi.Finale.Structures;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JaysAi.Finale.Visuals
{
    public partial class AIOverlay : Window
    {
        private readonly DispatcherTimer _updateTimer;
        private readonly OverlaySignal _overlaySignal;
        private readonly TargetMemory _targetMemory;

        public AIOverlay(TargetMemory targetMemory, OverlaySignal overlaySignal)
        {
            InitializeComponent();
            _targetMemory = targetMemory ?? throw new ArgumentNullException(nameof(targetMemory));
            _overlaySignal = overlaySignal ?? throw new ArgumentNullException(nameof(overlaySignal));

            _updateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(15)
            };
            _updateTimer.Tick += UpdateOverlay;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MakeWindowClickThrough();
            _updateTimer.Start();
        }

        private void MakeWindowClickThrough()
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            int extendedStyle = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE);
            NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE, extendedStyle | NativeMethods.WS_EX_TRANSPARENT | NativeMethods.WS_EX_TOOLWINDOW);
        }

        private void UpdateOverlay(object sender, EventArgs e)
        {
            OverlayCanvas.Children.Clear();

            if (!_overlaySignal.ShouldRender)
                return;

            foreach (var target in _targetMemory.GetVisibleTargets())
            {
                DrawBox(target.LastKnownObject);
            }
        }

        private void DrawBox(DetectedObject obj)
        {
            var box = new Rectangle
            {
                Width = obj.BoundingBox.Width,
                Height = obj.BoundingBox.Height,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            Canvas.SetLeft(box, obj.BoundingBox.X);
            Canvas.SetTop(box, obj.BoundingBox.Y);

            OverlayCanvas.Children.Add(box);
        }
    }
}
