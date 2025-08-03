using System.Runtime.InteropServices;
using System.Windows;
using SkiaSharp;

namespace JaysAi.Finale.Utilit
{
    public static class ScreenHelper
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT { public int X; public int Y; }

        public static SKPoint GetCursorPosition()
        {
            GetCursorPos(out var point);
            return new SKPoint(point.X, point.Y);
        }

        public static SKPoint GetCenter()
        {
            var width = SystemParameters.PrimaryScreenWidth;
            var height = SystemParameters.PrimaryScreenHeight;
            return new SKPoint((float)(width / 2), (float)(height / 2));
        }

        public static SKSize GetResolution()
        {
            return new SKSize(
                (float)SystemParameters.PrimaryScreenWidth,
                (float)SystemParameters.PrimaryScreenHeight
            );
        }
    }
}
