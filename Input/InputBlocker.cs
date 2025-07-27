using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class InputBlocker
    {
        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool fBlockIt);

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        public static void Block()
        {
            BlockInput(true);
        }

        public static void Unblock()
        {
            BlockInput(false);
        }

        public static TimeSpan GetIdleTime()
        {
            var lastInput = new LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO)) };

            if (GetLastInputInfo(ref lastInput))
            {
                uint idleTicks = (uint)Environment.TickCount - lastInput.dwTime;
                return TimeSpan.FromMilliseconds(idleTicks);
            }

            return TimeSpan.Zero;
        }
    }
}
