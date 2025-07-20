// File: System\Win32Helper.cs

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace JaysAi.Finale.SystemLogic
{
    public static class Win32Helper
    {
        public static void MakeClickthroughTransparent(Window window)
        {
            var hwnd = new WindowInteropHelper(window).EnsureHandle();

            int extendedStyle = Win32.GetWindowLong(hwnd, Win32.GWL_EXSTYLE);
            Win32.SetWindowLong(hwnd, Win32.GWL_EXSTYLE, extendedStyle | Win32.WS_EX_LAYERED | Win32.WS_EX_TRANSPARENT);
            Win32.SetLayeredWindowAttributes(hwnd, 0, 255, Win32.LWA_ALPHA);
        }

        public static void HideFromTaskbar(Window window)
        {
            var hwnd = new WindowInteropHelper(window).EnsureHandle();
            Win32.ShowWindow(hwnd, Win32.SW_HIDE);
        }

        public static void ShowInTaskbar(Window window)
        {
            var hwnd = new WindowInteropHelper(window).EnsureHandle();
            Win32.ShowWindow(hwnd, Win32.SW_SHOW);
        }
    }
}
