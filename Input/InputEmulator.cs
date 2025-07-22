//heavenly v3.0 – InputEmulator Signal Injector
using System;
using System.Runtime.InteropServices;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Input
{
    public class InputEmulator
    {
        private int _pendingMouseX;
        private int _pendingMouseY;
        private bool _hasPendingMove;

        public void MoveMouseRelative(int deltaX, int deltaY)
        {
            _pendingMouseX += deltaX;
            _pendingMouseY += deltaY;
            _hasPendingMove = true;
        }

        public void ApplyPendingInputs()
        {
            if (_hasPendingMove)
            {
                NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_MOVE, _pendingMouseX, _pendingMouseY, 0, 0);
                _hasPendingMove = false;
                _pendingMouseX = 0;
                _pendingMouseY = 0;
            }
        }

        public void ClickLeftMouse()
        {
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public void ClickRightMouse()
        {
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        public void PressKey(ushort keyCode)
        {
            NativeMethods.keybd_event((byte)keyCode, 0, 0, 0);
            NativeMethods.keybd_event((byte)keyCode, 0, NativeMethods.KEYEVENTF_KEYUP, 0);
        }
    }
}
