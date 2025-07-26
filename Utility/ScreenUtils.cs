// neural v3.0
using System;
using System.Linq;
using System.Drawing;                     // For System.Drawing.Size and Point
using System.Windows;                    // For WPF Window
using System.Windows.Interop;            // For WindowInteropHelper
using System.Windows.Forms;              // For Screen and Bounds

namespace JaysAi.Finale.Utility
{
    public static class ScreenUtils
    {
        /// <summary>
        /// Gets the primary screen resolution in DPI-aware pixels.
        /// </summary>
        public static System.Drawing.Size GetPrimaryScreenResolution()
        {
            var screen = System.Windows.Forms.Screen.PrimaryScreen;
            return new System.Drawing.Size(screen.Bounds.Width, screen.Bounds.Height);
        }

        /// <summary>
        /// Gets the working area (excluding taskbar) of the primary screen.
        /// </summary>
        public static System.Drawing.Rectangle GetPrimaryWorkingArea() => System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;

        /// <summary>
        /// Gets the screen that contains the specified window.
        /// </summary>
        public static System.Windows.Forms.Screen GetScreenFromWindow(Window window)
        {
            var interop = new WindowInteropHelper(window);
            return System.Windows.Forms.Screen.FromHandle(interop.Handle);
        }

        /// <summary>
        /// Gets the screen that contains the specified point.
        /// </summary>
        public static System.Windows.Forms.Screen GetScreenFromPoint(System.Drawing.Point point) => System.Windows.Forms.Screen.AllScreens
                .FirstOrDefault(predicate: s => s.Bounds.Contains(point))
                ?? Screen.PrimaryScreen;

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
