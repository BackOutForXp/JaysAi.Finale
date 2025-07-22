//heavenly v3.0 – Raw Input Listener
using System;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Input
{
    public class ControllerInputListener
    {
        public event Action<ControllerState>? OnControllerStateChanged;
        private ControllerInputPoller _poller;

        public ControllerInputListener()
        {
            _poller = new ControllerInputPoller();
            _poller.OnInputUpdated += HandlePollerInput;
        }

        private void HandlePollerInput(ControllerState state)
        {
            // Forward raw input updates
            OnControllerStateChanged?.Invoke(state);
        }

        public void Begin()
        {
            _poller.StartPolling();
        }

        public void End()
        {
            _poller.StopPolling();
        }
    }
}
