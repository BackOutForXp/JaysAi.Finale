// File: Input/MouseInputEmulator.cs
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;

namespace JaysAi.Finale.Input
{
    public static class MouseInputEmulator
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, nuint dwExtraInfo);

        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        private struct POINT
        {
            public int X;
            public int Y;
        }

        public static Vector2 GetCursorPosition()
        {
            GetCursorPos(out POINT point);
            return new Vector2(point.X, point.Y);
        }

        public static void MoveTo(Vector2 screenPos)
        {
            SetCursorPos((int)screenPos.X, (int)screenPos.Y);
        }

        public static void MoveBy(Vector2 delta)
        {
            Vector2 current = GetCursorPosition();
            MoveTo(current + delta);
        }

        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, nuint.Zero);
            Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, nuint.Zero);
        }

        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, nuint.Zero);
            Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, nuint.Zero);
        }

        public static void SmoothMove(Vector2 from, Vector2 to, float smoothing = 8f)
        {
            Vector2 delta = (to - from) / smoothing;
            MoveBy(delta);
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Pure Win32 calls with no legacy libraries
// ✅ Includes cursor read, move, click, and smooth aim
// ✅ Used by AimAssist, RecoilCompensator, TriggerBot
// TODO: Add custom acceleration curves or AI-based snapping
// ===================================================================
