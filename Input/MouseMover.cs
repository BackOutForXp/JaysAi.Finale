using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class MouseMover
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_MOVE = 0x0001;

        public static void Move(int dx, int dy)
        {
            mouse_event(MOUSEEVENTF_MOVE, dx, dy, 0, 0);
        }
    }
}
