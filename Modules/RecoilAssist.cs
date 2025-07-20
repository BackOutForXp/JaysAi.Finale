using System;
using System.Threading;
using System.Threading.Tasks;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Modules
{
    public static class RecoilAssist
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
            Console.WriteLine("Recoil Assist Enabled");
        }

        private static void Stop()
        {
            _cts?.Cancel();
            Console.WriteLine("Recoil Assist Disabled");
        }

        private static async Task Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (InputMonitor.IsLeftClickHeld())
                {
                    // Simulate slight downward movement
                    MouseMover.Move(0, 1); // 1px down
                    await Task.Delay(10, token);
                }
                else
                {
                    await Task.Delay(25, token);
                }
            }
        }
    }
}
