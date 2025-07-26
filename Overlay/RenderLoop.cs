// neural v3.0
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale.Overlay
{
    public sealed class RenderLoop : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private Task? _renderTask;
        private readonly Stopwatch _stopwatch = new();
        private readonly int _targetFps;
        private readonly Action _renderAction;

        public RenderLoop(Action renderAction, int targetFps = 144)
        {
            _renderAction = renderAction ?? throw new ArgumentNullException(nameof(renderAction));
            _targetFps = targetFps;
        }

        public void Start()
        {
            if (_renderTask != null && !_renderTask.IsCompleted)
                return;

            _stopwatch.Start();

            _renderTask = Task.Factory.StartNew(() =>
            {
                var frameTime = TimeSpan.FromMilliseconds(1000.0 / _targetFps);
                var token = _cancellationTokenSource.Token;

                while (!token.IsCancellationRequested)
                {
                    var start = _stopwatch.Elapsed;

                    try
                    {
                        _renderAction.Invoke();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[RenderLoop] Exception: {ex.Message}");
                    }

                    var elapsed = _stopwatch.Elapsed - start;
                    var delay = frameTime - elapsed;
                    if (delay > TimeSpan.Zero)
                        Thread.Sleep(delay);
                }

            }, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();

            try
            {
                _renderTask?.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                    Debug.WriteLine($"[RenderLoop] Stop Exception: {ex.Message}");
            }

            _stopwatch.Stop();
        }

        public void Dispose()
        {
            Stop();
            _cancellationTokenSource.Dispose();
        }
    }
}
