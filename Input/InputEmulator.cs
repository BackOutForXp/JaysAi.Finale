// File: Input/InputEmulator.cs
using System;
using System.Runtime.InteropServices;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public static class InputEmulator
    {
        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public nint dwExtraInfo;
        }

        const uint INPUT_MOUSE = 0;
        const uint MOUSEEVENTF_MOVE = 0x0001;

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static void MoveMouse(Vector2 delta)
        {
            if (delta == Vector2.Zero)
                return;

            var input = new INPUT
            {
                type = INPUT_MOUSE,
                mi = new MOUSEINPUT
                {
                    dx = (int)Math.Round(delta.X),
                    dy = (int)Math.Round(delta.Y),
                    dwFlags = MOUSEEVENTF_MOVE,
                    mouseData = 0,
                    time = 0,
                    dwExtraInfo = nint.Zero
                }
            };

            SendInput(1, new INPUT[] { input }, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
