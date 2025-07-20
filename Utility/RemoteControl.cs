using System;
using System.Threading;
using System.Windows.Input;

namespace JaysAi.Finale.Utility
{
    public static class RemoteControl
    {
        private static Thread? _thread;
        private static bool _running = false;

        public static void Start()
        {
            if (_running) return;

            _running = true;
            _thread = new Thread(Listener)
            {
                IsBackground = true
            };
            _thread.Start();
        }

        private static void Listener()
        {
            while (_running)
            {
                if (Keyboard.IsKeyDown(Key.F10))
                {
                    Console.WriteLine("[RemoteControl] F10 pressed - Shutting down.");
                    Environment.Exit(0);
                }
                else if (Keyboard.IsKeyDown(Key.F11))
                {
                    Console.WriteLine("[RemoteControl] F11 pressed - Restarting app...");
                    System.Diagnostics.Process.Start(Environment.ProcessPath!);
                    Environment.Exit(0);
                }
                else if (Keyboard.IsKeyDown(Key.F12))
                {
                    Console.WriteLine($"[RemoteControl] F12 pressed - Version: Monarch Mode v1.0");
                }

                Thread.Sleep(100); // debounce
            }
        }

        public static void Stop()
        {
            _running = false;
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Listens for global keybinds while loader runs
// ✅ F10 = Panic exit (kills loader immediately)
// ✅ F11 = Restart app
// ✅ F12 = Log version info (Monarch Mode v1.0)
// - [ ] Future: Map tier-level actions or hot-swaps (Owner Tier secret tools)
// ===================================================================
