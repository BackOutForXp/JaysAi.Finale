// neural v3.0
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public static class MouseSimulator
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint Type;
            public InputUnion U;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public MOUSEINPUT Mouse;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int Dx;
            public int Dy;
            public uint MouseData;
            public uint DwFlags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        private const uint INPUT_MOUSE = 0;
        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static void MoveBy(int deltaX, int deltaY)
        {
            var input = new INPUT
            {
                Type = INPUT_MOUSE,
                U = new InputUnion
                {
                    Mouse = new MOUSEINPUT
                    {
                        Dx = deltaX,
                        Dy = deltaY,
                        DwFlags = MOUSEEVENTF_MOVE,
                        MouseData = 0,
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, new[] { input }, Marshal.SizeOf<INPUT>());
        }

        public static void LeftClick()
        {
            MouseDownLeft();
            Thread.Sleep(10); // simulate click duration
            MouseUpLeft();
        }

        public static void RightClick()
        {
            MouseDownRight();
            Thread.Sleep(10);
            MouseUpRight();
        }

        public static void MouseDownLeft()
        {
            SendMouseEvent(MOUSEEVENTF_LEFTDOWN);
        }

        public static void MouseUpLeft()
        {
            SendMouseEvent(MOUSEEVENTF_LEFTUP);
        }

        public static void MouseDownRight()
        {
            SendMouseEvent(MOUSEEVENTF_RIGHTDOWN);
        }

        public static void MouseUpRight()
        {
            SendMouseEvent(MOUSEEVENTF_RIGHTUP);
        }

        private static void SendMouseEvent(uint flag)
        {
            var input = new INPUT
            {
                Type = INPUT_MOUSE,
                U = new InputUnion
                {
                    Mouse = new MOUSEINPUT
                    {
                        Dx = 0,
                        Dy = 0,
                        DwFlags = flag,
                        MouseData = 0,
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, new[] { input }, Marshal.SizeOf<INPUT>());
        }
    }
}
