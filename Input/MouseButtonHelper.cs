// File: Input/MouseButtonHelper.cs
using System.Runtime.InteropServices;

namespace JaysAi.Finale.Input
{
    public static class MouseButtonHelper
    {
        private const int VK_LBUTTON = 0x01;
        private const int VK_RBUTTON = 0x02;
        private const int VK_MBUTTON = 0x04;
        private const int VK_XBUTTON1 = 0x05;
        private const int VK_XBUTTON2 = 0x06;

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static bool IsLeftButtonDown() => (GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0;
        public static bool IsRightButtonDown() => (GetAsyncKeyState(VK_RBUTTON) & 0x8000) != 0;
        public static bool IsMiddleButtonDown() => (GetAsyncKeyState(VK_MBUTTON) & 0x8000) != 0;
        public static bool IsXButton1Down() => (GetAsyncKeyState(VK_XBUTTON1) & 0x8000) != 0;
        public static bool IsXButton2Down() => (GetAsyncKeyState(VK_XBUTTON2) & 0x8000) != 0;
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Simple and fast mouse button detection
// ✅ Fully native and stealth compatible
// ✅ Used in InputEmulator or TriggerBot logic
// ===================================================================
