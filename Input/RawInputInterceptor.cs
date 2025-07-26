// neural v3.0
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JaysAi.Finale.Input
{
    public static class RawInputInterceptor
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;
        private static IntPtr _keyboardHook = IntPtr.Zero;
        private static IntPtr _mouseHook = IntPtr.Zero;

        private static LowLevelProc? _keyboardProc;
        private static LowLevelProc? _mouseProc;

        public static void Initialize()
        {
            _keyboardProc = KeyboardCallback;
            _mouseProc = MouseCallback;

            _keyboardHook = SetHook(_keyboardProc, WH_KEYBOARD_LL);
            _mouseHook = SetHook(_mouseProc, WH_MOUSE_LL);
        }

        public static void Shutdown()
        {
            UnhookWindowsHookEx(_keyboardHook);
            UnhookWindowsHookEx(_mouseHook);
        }

        private static IntPtr SetHook(LowLevelProc proc, int hookId)
        {
            using var curProcess = System.Diagnostics.Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule!;
            return SetWindowsHookEx(hookId, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }

        private static IntPtr KeyboardCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Placeholder — Add logging or logic as needed
            return CallNextHookEx(_keyboardHook, nCode, wParam, lParam);
        }

        private static IntPtr MouseCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Placeholder — Add logic for mouse movement interception
            return CallNextHookEx(_mouseHook, nCode, wParam, lParam);
        }

        private delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string? lpModuleName);
    }
}
