using System;
using System.Threading;
using System.Threading.Tasks;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Modules
{
    public static class MovementAssist
    {
        private static bool _enabled;
        private static CancellationTokenSource? _cts;

        public static void Toggle(bool state)
        {
            if (_enabled == state)
                return;

            _enabled = state;

            if (_enabled)
                Start();
            else
                Stop();
        }

        private static void Start()
        {
            _cts = new CancellationTokenSource();
            Task.Run(() => Loop(_cts.Token));
            Console.WriteLine("Movement Assist Enabled");
        }

        private static void Stop()
        {
            _cts?.Cancel();
            Console.WriteLine("Movement Assist Disabled");
        }

        private static async Task Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (InputMonitor.IsKeyDown(System.Windows.Input.Key.Space))
                {
                    SimulateBunnyHop();
                }

                if (InputMonitor.IsKeyDown(System.Windows.Input.Key.LeftShift))
                {
                    SimulateAutoSprint();
                }

                await Task.Delay(30, token);
            }
        }

        private static void SimulateBunnyHop()
        {
            Console.WriteLine("[MovementAssist] Bunny hopping...");
            // TODO: Replace with actual jump/spam key input via SendInput or HID
        }

        private static void SimulateAutoSprint()
        {
            Console.WriteLine("[MovementAssist] Auto-sprinting...");
            // TODO: Trigger sprint key if not already running
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ To finalize this module:
// - [ ] Use SendInput or HID emulation to trigger keys
// - [x] Reads shift + space via InputMonitor
// - [ ] Future: Add crouch spam, slide cancel patterns (per-game configs)
// - [ ] Tier-lock via FeatureManager.CanUseMovementAssist()
// ===================================================================
