using System;
using System.Threading;
using System.Threading.Tasks;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Modules
{
    public static class TriggerBot
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
            Console.WriteLine("TriggerBot Enabled");
        }

        private static void Stop()
        {
            _cts?.Cancel();
            Console.WriteLine("TriggerBot Disabled");
        }

        private static async Task Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (IsTargetInCrosshair())
                {
                    MouseClicker.LeftClick();
                }

                await Task.Delay(25, token);
            }
        }

        private static bool IsTargetInCrosshair()
        {
            // TODO: Replace this placeholder with AI/memory logic
            return false;
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ To finalize this module:
// - [ ] Replace IsTargetInCrosshair() with:
//       • AI frame capture (YOLO)
//       • Memory read enemy crosshair ID
// - [ ] Sync with OverlayRenderer for debug drawing
// - [ ] Tier-lock to Owner via FeatureManager.CanUseTriggerBot()
// - [ ] Optionally tie cooldowns or delays per weapon
// ===================================================================
