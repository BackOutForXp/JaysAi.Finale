//heavenly v3.0 – InputInjector Module
using System.Runtime.InteropServices;
using System.Windows.Input;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Input
{
    public static class InputInjector
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint flags, int dx, int dy, uint data, UIntPtr extraInfo);

        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        public static void MoveMouse(int deltaX, int deltaY)
        {
            mouse_event(MOUSEEVENTF_MOVE, deltaX, deltaY, 0, UIntPtr.Zero);
        }

        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        }

        public static void PressKey(Key key)
        {
            KeyboardSimulator.PressKey(key);
        }

        public static void ReleaseKey(Key key)
        {
            KeyboardSimulator.ReleaseKey(key);
        }

        public static void PressAndReleaseKey(Key key)
        {
            KeyboardSimulator.PressKey(key);
            KeyboardSimulator.ReleaseKey(key);
        }
    }
}
