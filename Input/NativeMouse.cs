using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace JaysAi.Finale.Input
{
    public static class NativeMouse
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        public static void MoveRelative(int deltaX, int deltaY)
        {
            if (GetCursorPos(out POINT current))
            {
                int targetX = current.X + deltaX;
                int targetY = current.Y + deltaY;
                SetCursorPos(targetX, targetY);
            }
        }

        public static void MoveSmooth(int deltaX, int deltaY, int steps = 10, int delayMs = 1)
        {
            if (GetCursorPos(out POINT start))
            {
                for (int i = 1; i <= steps; i++)
                {
                    int x = start.X + deltaX * i / steps;
                    int y = start.Y + deltaY * i / steps;
                    SetCursorPos(x, y);
                    Thread.Sleep(delayMs);
                }
            }
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Low-level mouse movement for aim assist and recoil
// ✅ Supports smooth or instant movement
// ✅ Used by RecoilControl, AimSnap, StickAssist, etc.
// - [ ] Add acceleration curves / easing
// - [ ] Add safety limiter (max delta range)
// ===================================================================
