//heavenly v3.0 – Controller Poller
using JaysAi.Integration;
using System;
using System.Timers;

namespace JaysAi.Finale.Input
{
    public class ControllerInputPoller
    {
        private readonly Timer _pollTimer;
        private readonly IControllerBridge _bridge;
        private ControllerState _lastState;

        public event Action<ControllerState>? OnPoll;

        public ControllerInputPoller(IControllerBridge bridge, double pollIntervalMs = 10)
        {
            _bridge = bridge;
            _pollTimer = new Timer(pollIntervalMs);
            _pollTimer.Elapsed += (s, e) => Poll();
        }

        public void Start()
        {
            _pollTimer.Start();
        }

        public void Stop()
        {
            _pollTimer.Stop();
        }

        private void Poll()
        {
            var currentState = _bridge.GetCurrentState();

            if (!currentState.Equals(_lastState))
            {
                _lastState = currentState;
                OnPoll?.Invoke(currentState);
            }
        }
    }
}
