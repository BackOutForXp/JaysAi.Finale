// Neural v3.0 — MainLoop.cs
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Helpers;

namespace JaysAi.Finale.SystemLogic
{
    public class MainLoop
    {
        private static CancellationTokenSource _cts;
        private static Task _loopTask;
        private static readonly Stopwatch _frameTimer = new();

        public static bool IsRunning { get; private set; } = false;
        public static int TargetFps { get; set; } = 144;

        public static void Start()
        {
            if (IsRunning) return;

            _cts = new CancellationTokenSource();
            _loopTask = Task.Run(() => RunLoop(_cts.Token));
            IsRunning = true;
        }

        public static void Stop()
        {
            if (!IsRunning) return;

            _cts.Cancel();
            IsRunning = false;
        }

        private static async Task RunLoop(CancellationToken token)
        {
            var delay = TimeSpan.FromMilliseconds(1000.0 / TargetFps);

            while (!token.IsCancellationRequested)
            {
                _frameTimer.Restart();

                try
                {
                    // 1. Poll input
                    InputManager.Update();

                    // 2. Update AI logic
                    AiManager.Tick();

                    // 3. Run prediction
                    PredictionEngine.Tick();

                    // 4. Update assist modules
                    SnapAssistController.Tick();

                    // 5. Delay for framerate sync
                    _frameTimer.Stop();
                    var elapsed = _frameTimer.ElapsedMilliseconds;
                    var sleepTime = delay.TotalMilliseconds - elapsed;

                    if (sleepTime > 0)
                        await Task.Delay((int)sleepTime, token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MainLoop] {ex.Message}");
                }
            }
        }
    }
}
