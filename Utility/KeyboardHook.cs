// neural v3.0
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace JaysAi.Finale.Utility
{
    public sealed class KeyboardHook : IDisposable
    {
        private IntPtr _hookID = IntPtr.Zero;
        private NativeMethods.LowLevelKeyboardProc? _proc;

        public event EventHandler<KeyEventArgs>? KeyPressed;

        public void Install()
        {
            _proc = HookCallback;
            _hookID = NativeMethods.SetHook(_proc);
        }

        public void Uninstall()
        {
            NativeMethods.UnhookWindowsHookEx(_hookID);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int msg = wParam.ToInt32();
                var keyInfo = Marshal.PtrToStructure<NativeMethods.KBDLLHOOKSTRUCT>(lParam);

                if (msg == NativeMethods.WM_KEYDOWN || msg == NativeMethods.WM_SYSKEYDOWN)
                {
                    var key = KeyInterop.KeyFromVirtualKey((int)keyInfo.vkCode);
                    KeyPressed?.Invoke(this, new KeyEventArgs(Keyboard.PrimaryDevice, PresentationSource.FromVisual(App.Current.MainWindow), 0, key)
                    {
                        RoutedEvent = Keyboard.KeyDownEvent
                    });
                }
            }
            return NativeMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            Uninstall();
            GC.SuppressFinalize(this);
        }

        private static class NativeMethods
        {
            public const int WH_KEYBOARD_LL = 13;
            public const int WM_KEYDOWN = 0x0100;
            public const int WM_SYSKEYDOWN = 0x0104;

            public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

            [StructLayout(LayoutKind.Sequential)]
            public struct KBDLLHOOKSTRUCT
            {
                public uint vkCode;
                public uint scanCode;
                public uint flags;
                public uint time;
                public IntPtr dwExtraInfo;
            }

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll")]
            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetModuleHandle(string lpModuleName);

            public static IntPtr SetHook(LowLevelKeyboardProc proc)
            {
                using var curProcess = Process.GetCurrentProcess();
                using var curModule = curProcess.MainModule!;
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName!), 0);
            }
        }
    }
}
