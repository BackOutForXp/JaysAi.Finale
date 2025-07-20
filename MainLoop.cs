// Monarch v1.0 – MainLoop.cs
// ✅ Monarch Fix Checklist
// [x] Modular async game loop
// [x] Safe cancellation
// [x] Load-tick cycle ready for feature hooks
// [x] Ready for controller + ESP threading

using System;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale
{
    public class MainLoop
    {
        private CancellationTokenSource _cts;
        private Task _loopTask;

        public bool IsRunning => _loopTask is { IsCompleted: false };

        public void Start()
        {
            if (IsRunning) return;

            _cts = new CancellationTokenSource();
            _loopTask = Task.Run(() => Loop(_cts.Token), _cts.Token);
            Logger.Info("Main loop started.");
        }

        public void Stop()
        {
            if (!IsRunning) return;

            _cts.Cancel();
            Logger.Info("Main loop stopping...");
        }

        private async Task Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    // TODO: Plug in real update logic (ESP, AimAssist, etc.)
                    Logger.Debug("Tick...");

                    // Simulate update cycle
                    await Task.Delay(16, token); // ~60 FPS
                }
                catch (TaskCanceledException) { }
                catch (Exception ex)
                {
                    Logger.Error($"MainLoop error: {ex.Message}");
                }
            }

            Logger.Info("Main loop stopped.");
        }
    }
}
