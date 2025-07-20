//monarch v2.0
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace JaysAi.Finale.Input
{
    /// <summary>
    /// Sends simulated keypresses using the Windows SendInput API.
    /// Used for macro triggers, recoil assists, or stealth controls.
    /// </summary>
    public static class KeyboardPresser
    {
        private const int INPUT_KEYBOARD = 1;
        private const ushort KEYEVENTF_KEYUP = 0x0002;

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public int type;
            public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public nint dwExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        /// <summary>
        /// Sends a full press + release of a virtual key.
        /// </summary>
        /// <param name="vk">The virtual key code.</param>
        public static void TapKey(ushort vk)
        {
            PressKey(vk);
            Thread.Sleep(25); // Delay between press and release
            ReleaseKey(vk);
        }

        /// <summary>
        /// Sends a key press event.
        /// </summary>
        public static void PressKey(ushort vk)
        {
            var input = new INPUT
            {
                type = INPUT_KEYBOARD,
                ki = new KEYBDINPUT
                {
                    wVk = vk,
                    dwFlags = 0,
                    dwExtraInfo = nint.Zero
                }
            };
            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));
        }

        /// <summary>
        /// Sends a key release event.
        /// </summary>
        public static void ReleaseKey(ushort vk)
        {
            var input = new INPUT
            {
                type = INPUT_KEYBOARD,
                ki = new KEYBDINPUT
                {
                    wVk = vk,
                    dwFlags = KEYEVENTF_KEYUP,
                    dwExtraInfo = nint.Zero
                }
            };
            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
