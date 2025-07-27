// Neural v3.1
using JaysAi.Finale.Input;
using System;
using System.Timers;
using SharpDX.XInput;

namespace JaysAi.Finale.Input
{
    public class InputListener
    {
        private readonly Controller _controller;
        private readonly Timer _pollTimer;
        private readonly ControllerState _currentState;

        public event Action<ControllerState>? OnStateUpdated;

        public InputListener()
        {
            _controller = new Controller(UserIndex.One);
            _pollTimer = new Timer(15); // Poll every 15ms (≈ 66 FPS)
            _pollTimer.Elapsed += PollInput;
            _currentState = new ControllerState();
        }

        public void Start()
        {
            _pollTimer.Start();
        }

        public void Stop()
        {
            _pollTimer.Stop();
        }

        private void PollInput(object? sender, ElapsedEventArgs e)
        {
            if (!_controller.IsConnected)
            {
                _currentState.Reset();
                _currentState.IsConnected = false;
                OnStateUpdated?.Invoke(_currentState);
                return;
            }

            var state = _controller.GetState();
            var gamepad = state.Gamepad;

            _currentState.LeftStick = new System.Numerics.Vector2(
                NormalizeThumb(gamepad.LeftThumbX),
                NormalizeThumb(gamepad.LeftThumbY)
            );

            _currentState.RightStick = new System.Numerics.Vector2(
                NormalizeThumb(gamepad.RightThumbX),
                NormalizeThumb(gamepad.RightThumbY)
            );

            _currentState.LeftTrigger = gamepad.LeftTrigger / 255f;
            _currentState.RightTrigger = gamepad.RightTrigger / 255f;

            _currentState.A = (gamepad.Buttons & GamepadButtonFlags.A) != 0;
            _currentState.B = (gamepad.Buttons & GamepadButtonFlags.B) != 0;
            _currentState.X = (gamepad.Buttons & GamepadButtonFlags.X) != 0;
            _currentState.Y = (gamepad.Buttons & GamepadButtonFlags.Y) != 0;

            _currentState.DPadUp = (gamepad.Buttons & GamepadButtonFlags.DPadUp) != 0;
            _currentState.DPadDown = (gamepad.Buttons & GamepadButtonFlags.DPadDown) != 0;
            _currentState.DPadLeft = (gamepad.Buttons & GamepadButtonFlags.DPadLeft) != 0;
            _currentState.DPadRight = (gamepad.Buttons & GamepadButtonFlags.DPadRight) != 0;

            _currentState.Start = (gamepad.Buttons & GamepadButtonFlags.Start) != 0;
            _currentState.Back = (gamepad.Buttons & GamepadButtonFlags.Back) != 0;

            _currentState.LeftBumper = (gamepad.Buttons & GamepadButtonFlags.LeftShoulder) != 0;
            _currentState.RightBumper = (gamepad.Buttons & GamepadButtonFlags.RightShoulder) != 0;

            _currentState.LeftStickPressed = (gamepad.Buttons & GamepadButtonFlags.LeftThumb) != 0;
            _currentState.RightStickPressed = (gamepad.Buttons & GamepadButtonFlags.RightThumb) != 0;

            _currentState.IsConnected = true;

            OnStateUpdated?.Invoke(_currentState);
        }

        private static float NormalizeThumb(short value)
        {
            const float max = 32767f;
            return Math.Clamp(value / max, -1f, 1f);
        }
    }
}
