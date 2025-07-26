// neural v3.0
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace JaysAi.Finale.Input
{
    public static class KeyboardPresser
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        private const int INPUT_KEYBOARD = 1;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        public static void PressKey(char key)
        {
            ushort virtualKey = (ushort)VkKeyScan(key);
            var inputDown = new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = virtualKey,
                        dwFlags = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            var inputUp = new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = virtualKey,
                        dwFlags = KEYEVENTF_KEYUP,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            INPUT[] inputs = { inputDown, inputUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void HoldKey(char key, int durationMs)
        {
            ushort virtualKey = (ushort)VkKeyScan(key);
            var inputDown = new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = virtualKey,
                        dwFlags = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            var inputUp = new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = virtualKey,
                        dwFlags = KEYEVENTF_KEYUP,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, new[] { inputDown }, Marshal.SizeOf(typeof(INPUT)));
            Thread.Sleep(durationMs);
            SendInput(1, new[] { inputUp }, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
