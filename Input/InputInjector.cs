//monarch v2.0
using System;
using System.Runtime.InteropServices;
using System.Numerics;
using JaysAi.SystemLogic;

namespace JaysAi.Finale.Input
{
    public static class InputInjector
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, nuint dwExtraInfo);

        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        public static void InjectMouseMovement(Vector2 delta)
        {
            int dx = (int)delta.X;
            int dy = (int)delta.Y;
            mouse_event(MOUSEEVENTF_MOVE, dx, dy, 0, nuint.Zero);
        }

        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, nuint.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, nuint.Zero);
        }

        public static void InjectAim(Vector2 targetPosition, Vector2 screenCenter, float sensitivity = 1.0f)
        {
            Vector2 delta = (targetPosition - screenCenter) * sensitivity;
            InjectMouseMovement(delta);
        }

        public static void InjectPID(Vector2 error, float kp = 0.75f)
        {
            Vector2 output = error * kp;
            InjectMouseMovement(output);
        }
    }
}
