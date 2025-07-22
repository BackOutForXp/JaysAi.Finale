//heavenly v3.0 – InputMonitor
using System;
using System.Timers;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Input
{
    public class InputMonitor
    {
        private readonly Timer _pollTimer;
        private readonly ControllerInputPoller _controllerPoller;
        private readonly KeyboardPresser _keyboardPresser;
        private readonly InputStateLogger _stateLogger;

        public event Action<string>? OnInputDetected;

        public InputMonitor()
        {
            _controllerPoller = new ControllerInputPoller();
            _keyboardPresser = new KeyboardPresser();
            _stateLogger = new InputStateLogger();

            _pollTimer = new Timer(10); // 100 FPS poll rate
            _pollTimer.Elapsed += PollInputs;
            _pollTimer.AutoReset = true;
        }

        public void Start() => _pollTimer.Start();
        public void Stop() => _pollTimer.Stop();

        private void PollInputs(object? sender, ElapsedEventArgs e)
        {
            foreach (var binding in InputMap.KeyboardBinds)
            {
                if (_keyboardPresser.IsKeyPressed(binding.Value))
                {
                    _stateLogger.LogKey(binding.Key);
                    OnInputDetected?.Invoke(binding.Key);
                }
            }

            foreach (var binding in InputMap.ControllerBinds)
            {
                if (_controllerPoller.IsButtonPressed(binding.Value))
                {
                    _stateLogger.LogButton(binding.Key);
                    OnInputDetected?.Invoke(binding.Key);
                }
            }
        }
    }
}
