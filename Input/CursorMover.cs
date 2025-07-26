// neural v3.0
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public sealed class CursorMover
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        private float _sensitivity = 1.0f;

        public void SetSensitivity(float value)
        {
            _sensitivity = Math.Clamp(value, 0.1f, 10f);
        }

        public void Move(Vector2 delta)
        {
            if (delta == Vector2.Zero)
                return;

            if (!GetCursorPos(out var current))
                return;

            int newX = current.X + (int)(delta.X * _sensitivity);
            int newY = current.Y - (int)(delta.Y * _sensitivity); // Invert Y

            SetCursorPos(newX, newY);
        }
    }
}
