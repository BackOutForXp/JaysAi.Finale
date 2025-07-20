//monarch v1.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JaysAi.Finale.Utility
{
    public class KeyboardHook : IDisposable
    {
        public event Action<Keys> KeyPressed;

        private nint _hookId = nint.Zero;
        private NativeMethods.LowLevelKeyboardProc _proc;

        public void Start()
        {
            _proc = HookCallback;
            _hookId = NativeMethods.SetHook(_proc);
        }

        public void Stop()
        {
            NativeMethods.UnhookWindowsHookEx(_hookId);
        }

        private nint HookCallback(int nCode, nint wParam, nint lParam)
        {
            if (nCode >= 0 &&
                (wParam == NativeMethods.WM_KEYDOWN || wParam == NativeMethods.WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                KeyPressed?.Invoke((Keys)vkCode);
            }
            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            Stop();
        }

        private static class NativeMethods
        {
            public const int WH_KEYBOARD_LL = 13;
            public const int WM_KEYDOWN = 0x0100;
            public const int WM_SYSKEYDOWN = 0x0104;

            public delegate nint LowLevelKeyboardProc(int nCode, nint wParam, nint lParam);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern nint SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, nint hMod, uint dwThreadId);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(nint hhk);

            [DllImport("user32.dll")]
            public static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

            [DllImport("kernel32.dll")]
            public static extern nint GetModuleHandle(string lpModuleName);

            public static nint SetHook(LowLevelKeyboardProc proc)
            {
                using Process curProcess = Process.GetCurrentProcess();
                using ProcessModule curModule = curProcess.MainModule;
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
    }
}
