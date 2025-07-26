// neural v3.0
using JaysAi.Finale.Input.Handlers;
using JaysAi.Finale.Input.Models;
using System;
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public sealed class InputHandler
    {
        private readonly IInputDevice _device;
        private readonly InputNormalizer _normalizer;
        private readonly InputFilter _filter;
        private readonly InputStateLogger _logger;

        public event Action<ControllerInputState>? OnProcessedInput;

        public InputHandler(IInputDevice device, InputNormalizer normalizer, InputFilter filter, InputStateLogger logger)
        {
            _device = device;
            _normalizer = normalizer;
            _filter = filter;
            _logger = logger;

            _device.InputReceived += HandleInput;
        }

        private void HandleInput(ControllerInputState rawState)
        {
            var normalized = _normalizer.Normalize(rawState);
            var filtered = _filter.Apply(normalized);

            _logger.LogState(filtered);
            OnProcessedInput?.Invoke(filtered);
        }

        public void Detach()
        {
            _device.InputReceived -= HandleInput;
        }
    }
}
