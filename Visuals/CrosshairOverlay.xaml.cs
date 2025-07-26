// neural v3.0
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;
using Rectangle = System.Windows.Shapes.Rectangle; // ✅ Correct Rectangle namespace

namespace JaysAi.Finale.Overlay
{
    public partial class CrosshairOverlay : System.Windows.Controls.UserControl
    {
        private System.Windows.Shapes.Rectangle _horizontalLine;
        private Rectangle _verticalLine;

        public bool IsCrosshairVisible
        {
            get => this.Visibility == Visibility.Visible;
            set => this.Visibility = value ? Visibility.Visible : Visibility.Hidden;
        }

        public System.Windows.Media.Brush CrosshairColor
        {
            get => _horizontalLine?.Fill;
            set
            {
                if (_horizontalLine != null) _horizontalLine.Fill = value;
                if (_verticalLine != null) _verticalLine.Fill = value;
            }
        }

        public double CrosshairLength
        {
            get => _horizontalLine?.Width ?? 40;
            set
            {
                if (_horizontalLine != null) _horizontalLine.Width = value;
                if (_verticalLine != null) _verticalLine.Height = value;
            }
        }

        public double CrosshairThickness
        {
            get => _horizontalLine?.Height ?? 2;
            set
            {
                if (_horizontalLine != null) _horizontalLine.Height = value;
                if (_verticalLine != null) _verticalLine.Width = value;
            }
        }

        public CrosshairOverlay()
        {
            InitializeComponent();
            InitializeCrosshairElements();
        }

        private void InitializeCrosshairElements()
        {
            _horizontalLine = FindName("HorizontalLine") as Rectangle;
            _verticalLine = FindName("VerticalLine") as System.Windows.Shapes.Rectangle;

            CrosshairColor = System.Windows.Media.Brushes.Red;
            CrosshairLength = 40;
            CrosshairThickness = 2;
            IsCrosshairVisible = true;
        }

        public void ToggleVisibility()
        {
            IsCrosshairVisible = !IsCrosshairVisible;
        }

        public void SetColor(Color color)
        {
            CrosshairColor = new SolidColorBrush(color);
        }

        public void SetSize(double length, double thickness)
        {
            CrosshairLength = length;
            CrosshairThickness = thickness;
        }
    }
}

