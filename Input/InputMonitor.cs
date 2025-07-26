// neural v3.0
using JaysAi.Finale.Input.Events;
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.SystemLogic.Logging;
using JaysAi.Finale.Utility;
using System;
using System.Timers;

namespace JaysAi.Finale.Input
{
    public sealed class InputMonitor : IDisposable
    {
        private readonly Timer _pollTimer;
        private ControllerInputState _lastState;
        private readonly IInputSource _inputSource;

        public event EventHandler<ControllerInputEventArgs>? InputChanged;

        public InputMonitor(IInputSource inputSource, double pollIntervalMs = 16.67) // ~60Hz
        {
            _inputSource = inputSource ?? throw new ArgumentNullException(nameof(inputSource));
            _pollTimer = new Timer(pollIntervalMs);
            _pollTimer.Elapsed += PollInput;
            _pollTimer.AutoReset = true;
            _lastState = new ControllerInputState();
        }

        public void Start()
        {
            Logger.Info("InputMonitor started.");
            _pollTimer.Start();
        }

        public void Stop()
        {
            Logger.Info("InputMonitor stopped.");
            _pollTimer.Stop();
        }

        private void PollInput(object? sender, ElapsedEventArgs e)
        {
            var currentState = _inputSource.GetCurrentState();
            if (!_lastState.Equals(currentState))
            {
                InputChanged?.Invoke(this, new ControllerInputEventArgs(currentState));
                _lastState = currentState;
                Logger.Trace("InputMonitor: Detected input change.");
            }
        }

        public void Dispose()
        {
            _pollTimer.Stop();
            _pollTimer.Dispose();
            Logger.Info("InputMonitor disposed.");
        }
    }
}
