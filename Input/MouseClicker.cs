// File: Input/MouseClicker.cs
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace JaysAi.Finale.Input
{
    public static class MouseClicker
    {
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, nuint dwExtraInfo);

        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, nuint.Zero);
            Thread.Sleep(15);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, nuint.Zero);
        }

        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, nuint.Zero);
            Thread.Sleep(15);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, nuint.Zero);
        }

        public static void HoldLeftClick(int durationMs)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, nuint.Zero);
            Thread.Sleep(durationMs);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, nuint.Zero);
        }

        public static void SpamClick(int count, int delayMs = 50)
        {
            for (int i = 0; i < count; i++)
            {
                LeftClick();
                Thread.Sleep(delayMs);
            }
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Core click emulation for TriggerBot, AssistFire
// ✅ Supports tap, hold, and spam clicks
// ✅ Pure Win32: no Drawing/Forms dependencies
// TODO: Add auto-fire rate tuning or lockout toggle
// ===================================================================
