using System;
using System.Runtime.InteropServices;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public class InputEmulator
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        private const uint INPUT_MOUSE = 0;
        private const uint MOUSEEVENTF_MOVE = 0x0001;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public void MoveMouse(Vector2 offset)
        {
            var input = new INPUT
            {
                type = INPUT_MOUSE,
                u = new InputUnion
                {
                    mi = new MOUSEINPUT
                    {
                        dx = (int)offset.X,
                        dy = (int)offset.Y,
                        dwFlags = MOUSEEVENTF_MOVE,
                        mouseData = 0,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, new[] { input }, Marshal.SizeOf(typeof(INPUT)));
        }

        public void MoveAnalog(Vector2 offset)
        {
            // Placeholder for controller analog movement emulation
            // Real implementation would use vJoy, ViGEm, or XInput wrappers
            Console.WriteLine($"[Analog] Simulated stick move: {offset}");
        }
    }
}
