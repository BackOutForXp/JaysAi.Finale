//monarch v2.1 – Input Event Listener
using System;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerInputState
    {
        public bool A;
        public bool B;
        public bool X;
        public bool Y;
        public bool LB;
        public bool RB;
        public bool LT;
        public bool RT;
        public bool DPadUp;
        public bool DPadDown;
        public bool DPadLeft;
        public bool DPadRight;
        public bool Start;
        public bool Back;

        public float LeftStickX;
        public float LeftStickY;
        public float RightStickX;
        public float RightStickY;

        public bool IsAnyButtonPressed =>
            A || B || X || Y || LB || RB || LT || RT || DPadUp || DPadDown || DPadLeft || DPadRight || Start || Back;

        public override string ToString()
        {
            return $"[A:{A} B:{B} X:{X} Y:{Y} L:{LeftStickX},{LeftStickY} R:{RightStickX},{RightStickY}]";
        }
    }
}
