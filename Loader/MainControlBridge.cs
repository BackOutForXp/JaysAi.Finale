// neural v3.0
using JaysAi.Finale.Hardware;
using JaysAi.Finale.Input;
using JaysAi.Finale.Input.Events;
using JaysAi.Finale.Signals;
using System;
using System.Windows.Input;

namespace JaysAi.Finale.Integration
{
    public sealed class MainControlBridge : IDisposable
    {
        private readonly ControllerInputListener _controllerListener;
        private readonly SignalBus _signalBus;
        private readonly IInputMonitor _inputMonitor;

        public event EventHandler<ControllerInputEventArgs>? OnControllerInput;
        public event EventHandler<KeyboardEventArgs>? OnKeyboardInput;

        public MainControlBridge(
            ControllerInputListener controllerListener,
            SignalBus signalBus,
            IInputMonitor inputMonitor)
        {
            _controllerListener = controllerListener;
            _signalBus = signalBus;
            _inputMonitor = inputMonitor;

            _controllerListener.InputReceived += HandleControllerInput;
            _inputMonitor.KeyboardEvent += HandleKeyboardInput;
        }

        private void HandleControllerInput(object? sender, ControllerInputEventArgs e)
        {
            OnControllerInput?.Invoke(this, e);
            _signalBus.Broadcast(e); // bridge to signal system
        }

        private void HandleKeyboardInput(object? sender, KeyboardEventArgs e)
        {
            OnKeyboardInput?.Invoke(this, e);
            _signalBus.Broadcast(e); // bridge to signal system
        }

        public void Dispose()
        {
            _controllerListener.InputReceived -= HandleControllerInput;
            _inputMonitor.KeyboardEvent -= HandleKeyboardInput;
        }
    }
}
