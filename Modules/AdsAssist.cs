using System;
using System.Threading;
using System.Threading.Tasks;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Modules
{
    public static class AdsAssist
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
            Console.WriteLine("ADS Assist Enabled");
        }

        private static void Stop()
        {
            _cts?.Cancel();
            MouseClicker.RightUp(); // safety: release right click
            Console.WriteLine("ADS Assist Disabled");
        }

        private static async Task Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                bool triggerHeld = InputMonitor.IsLeftClickHeld();

                if (triggerHeld)
                {
                    MouseClicker.RightDown();
                }
                else
                {
                    MouseClicker.RightUp();
                }

                await Task.Delay(16, token); // ~60FPS
            }
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ To finalize this module:
// - [ ] Improve target detection (check enemy proximity)
// - [x] Ties into InputMonitor (detects left-click firing)
// - [ ] Add delay/smart ADS logic for future profiles
// - [ ] Tier-lock via FeatureManager.CanUseAdsAssist()
// ===================================================================
