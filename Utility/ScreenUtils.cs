// neural v3.0
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace JaysAi.Finale.Utility
{
    public static class ScreenUtils
    {
        /// <summary>
        /// Gets the primary screen resolution in DPI-aware pixels.
        /// </summary>
        public static Size GetPrimaryScreenResolution()
        {
            var screen = Screen.PrimaryScreen;
            return new Size(screen.Bounds.Width, screen.Bounds.Height);
        }

        /// <summary>
        /// Gets the working area (excluding taskbar) of the primary screen.
        /// </summary>
        public static Rectangle GetPrimaryWorkingArea()
        {
            return Screen.PrimaryScreen.WorkingArea;
        }

        /// <summary>
        /// Gets the screen that contains the specified window.
        /// </summary>
        public static Screen GetScreenFromWindow(Window window)
        {
            var interop = new System.Windows.Interop.WindowInteropHelper(window);
            return Screen.FromHandle(interop.Handle);
        }

        /// <summary>
        /// Gets the screen that contains the specified point.
        /// </summary>
        public static Screen GetScreenFromPoint(System.Drawing.Point point)
        {
            return Screen.AllScreens.FirstOrDefault(s => s.Bounds.Contains(point))
                ?? Screen.PrimaryScreen;
        }

        /// <summary>
        /// Returns the screen DPI scaling factor.
        /// </summary>
        public static double GetDpiScale()
        {
            using var g = Graphics.FromHwnd(IntPtr.Zero);
            return g.DpiX / 96.0; // 96 DPI is standard
        }

        /// <summary>
        /// Converts a value from device-independent units (WPF) to screen pixels.
        /// </summary>
        public static int DipToPixels(double dip)
        {
            return (int)(dip * GetDpiScale());
        }

        /// <summary>
        /// Converts screen pixels to WPF units (DIP).
        /// </summary>
        public static double PixelsToDip(int pixels)
        {
            return pixels / GetDpiScale();
        }
    }
}
