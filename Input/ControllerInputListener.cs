//neural v3.0
using System;
using System.Collections.Concurrent;
using System.Threading;
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.Input.Events;

namespace JaysAi.Finale.Input
{
    public sealed class ControllerInputListener : IDisposable
    {
        private readonly Timer _pollingTimer;
        private readonly IControllerInputSource _inputSource;
        private readonly ConcurrentDictionary<int, ControllerInputState> _lastStates;
        private readonly int _pollingIntervalMs;

        public event EventHandler<ControllerInputChangedEventArgs>? InputChanged;

        public ControllerInputListener(IControllerInputSource inputSource, int pollingIntervalMs = 10)
        {
            _inputSource = inputSource ?? throw new ArgumentNullException(nameof(inputSource));
            _pollingIntervalMs = pollingIntervalMs;
            _lastStates = new ConcurrentDictionary<int, ControllerInputState>();

            _pollingTimer = new Timer(PollInputs, null, 0, _pollingIntervalMs);
        }

        private void PollInputs(object? state)
        {
            foreach (var controllerId in _inputSource.ConnectedControllerIds)
            {
                var currentState = _inputSource.GetState(controllerId);
                if (_lastStates.TryGetValue(controllerId, out var previousState))
                {
                    if (!currentState.Equals(previousState))
                    {
                        _lastStates[controllerId] = currentState;
                        InputChanged?.Invoke(this, new ControllerInputChangedEventArgs(controllerId, currentState));
                    }
                }
                else
                {
                    _lastStates[controllerId] = currentState;
                    InputChanged?.Invoke(this, new ControllerInputChangedEventArgs(controllerId, currentState));
                }
            }
        }

        public void Dispose()
        {
            _pollingTimer.Dispose();
        }
    }
}
