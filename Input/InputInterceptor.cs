//heavenly v3.0 – InputInterceptor
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class InputInterceptor
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;

        private static IntPtr _keyboardHookId = IntPtr.Zero;
        private static IntPtr _mouseHookId = IntPtr.Zero;

        private static LowLevelProc _keyboardProc = KeyboardHookCallback;
        private static LowLevelProc _mouseProc = MouseHookCallback;

        public static void Enable()
        {
            _keyboardHookId = SetHook(_keyboardProc, WH_KEYBOARD_LL);
            _mouseHookId = SetHook(_mouseProc, WH_MOUSE_LL);
        }

        public static void Disable()
        {
            UnhookWindowsHookEx(_keyboardHookId);
            UnhookWindowsHookEx(_mouseHookId);
        }

        private static IntPtr SetHook(LowLevelProc proc, int hookId)
        {
            using var curProcess = Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule;
            return SetWindowsHookEx(hookId, proc,
                GetModuleHandle(curModule?.ModuleName), 0);
        }

        private delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Placeholder: Intercept key input logic here
            return CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
        }

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Placeholder: Intercept mouse input logic here
            return CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
