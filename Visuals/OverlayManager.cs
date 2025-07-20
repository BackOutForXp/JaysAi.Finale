//monarch v2.1 – Visual overlay lifecycle controller
using JaysAi.Finale.AI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale.Visuals
{
    public static class OverlayManager
    {
        private static CancellationTokenSource? _cts;
        private static Task? _renderLoopTask;
        private static bool _isRunning = false;

        public static void Start()
        {
            if (_isRunning)
                return;

            _cts = new CancellationTokenSource();
            _renderLoopTask = Task.Run(() => RenderLoop(_cts.Token));
            _isRunning = true;

            OverlaySignal.UpdateStatus("Overlay started.");
        }

        public static void Stop()
        {
            if (!_isRunning)
                return;

            _cts?.Cancel();
            _isRunning = false;

            OverlaySignal.UpdateStatus("Overlay stopped.");
        }

        private static async Task RenderLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                // Draw everything to console (placeholder until DX or Skia)
                AiOverlay.Draw();
                OverlayDrawer.DrawAll();

                await Task.Delay(33, token); // ~30 FPS
            }
        }
    }
}
