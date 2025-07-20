using System.Runtime.InteropServices;
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public static class InputMonitor
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static bool IsKeyDown(Key key)
        {
            int vKey = KeyInterop.VirtualKeyFromKey(key);
            return (GetAsyncKeyState(vKey) & 0x8000) != 0;
        }

        public static bool IsLeftClickHeld()
        {
            return (GetAsyncKeyState(0x01) & 0x8000) != 0; // VK_LBUTTON
        }

        public static bool IsRightClickHeld()
        {
            return (GetAsyncKeyState(0x02) & 0x8000) != 0; // VK_RBUTTON
        }

        public static bool IsKeyComboDown(Key key1, Key key2)
        {
            return IsKeyDown(key1) && IsKeyDown(key2);
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Used by: AdsAssist, MovementAssist
// - [x] Supports key down detection
// - [x] Supports mouse left/right click hold check
// - [ ] Consider adding shift/toggle detection for macro logic
// - [ ] Could support controller polling in future (ViGEm/HID)
// ===================================================================
