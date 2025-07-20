using System;
using System.Threading;
using System.Threading.Tasks;
using JaysAi.Finale.Config;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Modules
{
    public static class AntiRecoil
    {
        private static bool _enabled = false;
        private static CancellationTokenSource? _recoilTokenSource;

        public static void Toggle(bool state)
        {
            _enabled = state;

            if (_enabled)
                Start();
            else
                Stop();
        }

        private static void Start()
        {
            _recoilTokenSource = new CancellationTokenSource();
            var token = _recoilTokenSource.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    ApplyRecoilCompensation();
                    await Task.Delay(ConfigManager.Current.RecoilDelay);
                }
            }, token);
        }

        private static void Stop()
        {
            _recoilTokenSource?.Cancel();
        }

        private static void ApplyRecoilCompensation()
        {
            if (!IsFiring()) return;

            int pullAmount = ConfigManager.Current.RecoilStrength;
            InputHandler.MoveMouse(0, pullAmount); // Downward nudge
        }

        private static bool IsFiring()
        {
            // TODO: Replace with real fire detection (mouse down or trigger hold)
            return true;
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Provides recoil compensation using vertical nudge
// ✅ Runs in background thread with delay config
// ✅ Integrates with InputHandler and ConfigManager
// - [ ] Replace IsFiring with real trigger detection
// - [ ] Add XY patterns per weapon (COD, Apex, R6)
// - [ ] Hook into Cronus or GPC output later
// ===================================================================
