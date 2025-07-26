//heavenly v3.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.SystemLogic
{
    public static class ScreenManager
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        public static Screen GetPrimaryScreen() => Screen.PrimaryScreen;

        public static IEnumerable<Screen> GetAllScreens() => Screen.AllScreens;

        public static Screen GetScreenFromPoint(Point point)
        {
            var drawingPoint = new System.Drawing.Point((int)point.X, (int)point.Y);
            return Screen.FromPoint(drawingPoint);
        }

        public static Screen GetScreenFromWindow(Window window)
        {
            var helper = new WindowInteropHelper(window);
            return Screen.FromHandle(helper.Handle);
        }

        public static Screen GetScreenFromCursor()
        {
            GetCursorPos(out POINT point);
            var drawingPoint = new System.Drawing.Point(point.X, point.Y);
            return Screen.FromPoint(drawingPoint);
        }

        public static Rectangle GetScreenBounds(Screen screen)
        {
            return screen.Bounds;
        }

        public static Screen GetLargestScreen()
        {
            return Screen.AllScreens.OrderByDescending(s => s.Bounds.Width * s.Bounds.Height).FirstOrDefault();
        }

        public static bool IsPointOnAnyScreen(Point point)
        {
            var drawingPoint = new System.Drawing.Point((int)point.X, (int)point.Y);
            return Screen.AllScreens.Any(screen => screen.Bounds.Contains(drawingPoint));
        }

        public static string GetScreenDebugInfo()
        {
            var info = new List<string>();
            foreach (var screen in Screen.AllScreens)
            {
                info.Add($"[{screen.DeviceName}] {screen.Bounds.Width}x{screen.Bounds.Height} | Primary: {screen.Primary}");
            }

            return string.Join("\n", info);
        }
    }
}
