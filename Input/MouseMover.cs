//monarch v2.1 – Fully Refactored & Synced

using global::System;
using global::System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class MouseMover
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        public static void MoveToScreenPosition(float x, float y)
        {
            int targetX = (int)Math.Round(x);
            int targetY = (int)Math.Round(y);
            SetCursorPos(targetX, targetY);
        }
    }
}
