//neural v3.0
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public class ControllerInputPoller : IDisposable
    {
        private readonly IInputDevice _device;
        private readonly Action<ControllerInputState> _onInputUpdate;
        private readonly CancellationTokenSource _cts;
        private Task? _pollingTask;
        private bool _isRunning;

        public int PollingIntervalMs { get; set; } = 5;

        public ControllerInputPoller(IInputDevice device, Action<ControllerInputState> onInputUpdate)
        {
            _device = device ?? throw new ArgumentNullException(nameof(device));
            _onInputUpdate = onInputUpdate ?? throw new ArgumentNullException(nameof(onInputUpdate));
            _cts = new CancellationTokenSource();
        }

        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;
            _pollingTask = Task.Run(() => PollLoop(_cts.Token));
        }

        private async Task PollLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var inputState = _device.ReadState();
                    _onInputUpdate.Invoke(inputState);
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Input poll failed: {ex.Message}");
                }

                await Task.Delay(PollingIntervalMs, token);
            }
        }

        public void Stop()
        {
            _cts.Cancel();
            _pollingTask?.Wait();
            _isRunning = false;
        }

        public void Dispose()
        {
            Stop();
            _cts.Dispose();
        }
    }
}
