//heavenly v3.0 – Controller Event Dispatcher
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Aimbot;

namespace JaysAi.Finale.Input
{
    public class ControllerBridge
    {
        public event Action<ControllerState>? OnStateChanged;
        public ControllerState CurrentState { get; private set; } = new();

        private readonly ControllerInputPoller _poller;

        public ControllerBridge()
        {
            _poller = new ControllerInputPoller();
            _poller.OnInputUpdated += HandleInputUpdate;
        }

        private void HandleInputUpdate(ControllerState newState)
        {
            if (!newState.Equals(CurrentState))
            {
                CurrentState = newState;
                OnStateChanged?.Invoke(CurrentState);
            }
        }

        public void StartListening()
        {
            _poller.StartPolling();
        }

        public void StopListening()
        {
            _poller.StopPolling();
        }

        public void InjectVirtualInput(ControllerState simulatedState)
        {
            // Optional: inject a simulated state (e.g. AI-controlled logic)
            CurrentState = simulatedState;
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}
