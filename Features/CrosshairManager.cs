// neural v3.0
using System.Windows.Media;
using JaysAi.Finale.Visuals;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System.Windows;

namespace JaysAi.Finale.Features
{
    public class CrosshairManager
    {
        private readonly Crosshair _crosshair;
        private bool _isInitialized;

        public CrosshairManager()
        {
            _crosshair = new Crosshair
            {
                Enabled = true,
                Thickness = 1.5,
                Length = 12,
                LineColor = Colors.LimeGreen,
                DynamicCentering = true
            };
        }

        public void Initialize()
        {
            if (_isInitialized) return;

            OverlayRenderer.RegisterPersistentDraw(DrawCrosshair);
            _isInitialized = true;
        }

        private void DrawCrosshair(DrawingContext context, double screenWidth, double screenHeight)
        {
            double centerX = screenWidth / 2;
            double centerY = screenHeight / 2;

            _crosshair.Draw(context, centerX, centerY);
        }

        public void SetEnabled(bool enabled)
        {
            _crosshair.Enabled = enabled;
        }

        public void UpdateColor(Color newColor)
        {
            _crosshair.LineColor = newColor;
        }

        public void SetDynamicTarget(Point? point)
        {
            _crosshair.TargetPoint = point;
        }

        public void UpdateStyle(double thickness, double length)
        {
            _crosshair.Thickness = thickness;
            _crosshair.Length = length;
        }
    }
}
