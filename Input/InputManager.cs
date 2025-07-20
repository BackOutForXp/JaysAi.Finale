using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Input
{
    public static class InputManager
    {
        private static Thread? _inputThread;
        private static bool _running;

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static void Start()
        {
            if (_running) return;

            _running = true;
            _inputThread = new Thread(PollInput) { IsBackground = true };
            _inputThread.Start();
            Logger.Info("[InputManager] Listening for hotkeys.");
        }

        public static void Stop()
        {
            _running = false;
            _inputThread?.Join();
            Logger.Warn("[InputManager] Stopped.");
        }

        private static void PollInput()
        {
            while (_running)
            {
                // Example: Toggle ESP with Insert key
                if (IsKeyDown(Key.Insert))
                {
                    ConfigManager.IsEspEnabled = !ConfigManager.IsEspEnabled;
                    Logger.Info($"ESP Toggled: {ConfigManager.IsEspEnabled}");
                    Thread.Sleep(200); // Debounce
                }

                // Future: Add AimAssist toggle, etc.
                Thread.Sleep(10); // Poll delay
            }
        }

        private static bool IsKeyDown(Key key)
        {
            return (GetAsyncKeyState(KeyInterop.VirtualKeyFromKey(key)) & 0x8000) != 0;
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Detects hotkeys using raw WinAPI (Insert toggle as example)
// ✅ Used for live feature switching without GUI
// ✅ Thread-safe, lightweight polling
// - [ ] Add hotkey rebinding in ConfigManager
// - [ ] Expand to detect controller input (via SharpDX / GPC)
// - [ ] Future: inject GPC script directly via Cronus Zen
// ===================================================================
