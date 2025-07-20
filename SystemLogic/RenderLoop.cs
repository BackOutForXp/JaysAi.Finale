// File: System\RenderLoop.cs

using System;
using System.Timers;

namespace JaysAi.Finale.SystemLogic
{
    public class RenderLoop
    {
        private readonly Timer _timer;
        private readonly Action _onFrame;

        public bool IsRunning => _timer.Enabled;

        public RenderLoop(Action onFrame, double fps = 60.0)
        {
            _onFrame = onFrame ?? throw new ArgumentNullException(nameof(onFrame));

            _timer = new Timer(1000.0 / fps)
            {
                AutoReset = true
            };
            _timer.Elapsed += OnRenderTick;
        }

        private void OnRenderTick(object? sender, ElapsedEventArgs e)
        {
            try
            {
                _onFrame.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RenderLoop] Frame error: {ex.Message}");
            }
        }

        public void Start()
        {
            if (!_timer.Enabled)
            {
                Console.WriteLine("[RenderLoop] Started");
                _timer.Start();
            }
        }

        public void Stop()
        {
            if (_timer.Enabled)
            {
                Console.WriteLine("[RenderLoop] Stopped");
                _timer.Stop();
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
            Console.WriteLine("[RenderLoop] Disposed");
        }
    }
}
