// File: Input/CursorMover.cs
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class CursorMover
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetCursorPos(out POINT point);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetCursorPos(int x, int y);

        public static Vector2 GetCursorPosition()
        {
            if (GetCursorPos(out POINT p))
                return new Vector2(p.X, p.Y);

            return Vector2.Zero;
        }

        public static void MoveCursorTo(Vector2 target)
        {
            SetCursorPos((int)target.X, (int)target.Y);
        }

        public static void MoveCursorSmooth(Vector2 current, Vector2 target, float smoothing)
        {
            if (smoothing <= 0)
            {
                MoveCursorTo(target);
                return;
            }

            Vector2 next = current + (target - current) / smoothing;
            MoveCursorTo(next);
        }

        public static void MoveRelative(Vector2 delta)
        {
            Vector2 current = GetCursorPosition();
            MoveCursorTo(current + delta);
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ No System.Drawing or Windows.Forms
// ✅ Uses Vector2 for precision compatibility
// ✅ Can be used with input emulators, aim modules
// TODO: Add acceleration/curve profile (optional advanced input)
// ===================================================================
