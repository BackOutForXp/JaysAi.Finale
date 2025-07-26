//heavenly v3.0
using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class MouseEmulator
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_MOVE = 0x0001;

        public static void MoveMouse(int deltaX, int deltaY)
        {
            mouse_event(MOUSEEVENTF_MOVE, (uint)deltaX, (uint)deltaY, 0, UIntPtr.Zero);
        }

        public static void SetPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void MoveSmooth(int targetX, int targetY, int durationMs = 100)
        {
            var startX = System.Windows.Forms.Cursor.Position.X;
            var startY = System.Windows.Forms.Cursor.Position.Y;
            int steps = Math.Max(durationMs / 10, 1);

            for (int i = 1; i <= steps; i++)
            {
                double t = (double)i / steps;
                int newX = (int)(startX + t * (targetX - startX));
                int newY = (int)(startY + t * (targetY - startY));
                SetCursorPos(newX, newY);
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
