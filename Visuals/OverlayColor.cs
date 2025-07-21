//monarch v2.1 – Overlay Color Definitions
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public static class OverlayColor
    {
        public static SolidColorBrush Red => new SolidColorBrush(Color.FromRgb(255, 0, 0));
        public static SolidColorBrush Green => new SolidColorBrush(Color.FromRgb(0, 255, 0));
        public static SolidColorBrush Blue => new SolidColorBrush(Color.FromRgb(0, 128, 255));
        public static SolidColorBrush Yellow => new SolidColorBrush(Color.FromRgb(255, 255, 0));
        public static SolidColorBrush Orange => new SolidColorBrush(Color.FromRgb(255, 165, 0));
        public static SolidColorBrush Purple => new SolidColorBrush(Color.FromRgb(128, 0, 128));
        public static SolidColorBrush Cyan => new SolidColorBrush(Color.FromRgb(0, 255, 255));
        public static SolidColorBrush White => new SolidColorBrush(Color.FromRgb(255, 255, 255));
        public static SolidColorBrush Black => new SolidColorBrush(Color.FromRgb(0, 0, 0));
        public static SolidColorBrush Gray => new SolidColorBrush(Color.FromRgb(128, 128, 128));
    }
}
