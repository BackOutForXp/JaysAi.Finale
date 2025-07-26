// neural v3.0
using JaysAi.Finale.Input.Interfaces;
using JaysAi.Finale.Input.Handlers;
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.Input.Devices;
using JaysAi.Finale.SystemLogic.Signals;
using System;

namespace JaysAi.Finale.Input
{
    public class InputManager : IDisposable
    {
        private IInputHandler _inputHandler;
        private readonly ControllerSignalBus _signalBus;
        private bool _disposed;

        public InputManager(IInputHandler? customHandler = null)
        {
            _inputHandler = customHandler ?? InputHandlerFactory.CreateDefault();
            _signalBus = ControllerSignalBus.Instance;

            _inputHandler.InputReceived += OnInputReceived;
        }

        private void OnInputReceived(object? sender, ControllerInputState state)
        {
            _signalBus.Broadcast(state); // Unified bus signal to other systems
        }

        public void SwitchInputHandler(IInputHandler newHandler)
        {
            if (_inputHandler == newHandler)
                return;

            _inputHandler.InputReceived -= OnInputReceived;
            _inputHandler = newHandler;
            _inputHandler.InputReceived += OnInputReceived;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _inputHandler.InputReceived -= OnInputReceived;
            _disposed = true;
        }
    }
}
