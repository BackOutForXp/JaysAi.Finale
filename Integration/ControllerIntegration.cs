// neural v3.0
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.Integration
{
    public class ControllerIntegration
    {
        private readonly IInputSource _inputSource;

        public event Action<ControllerInputState>? OnInputCaptured;

        public ControllerIntegration(IInputSource inputSource)
        {
            _inputSource = inputSource ?? throw new ArgumentNullException(nameof(inputSource));
        }

        public void PollInput()
        {
            try
            {
                var state = _inputSource.GetInputState();
                OnInputCaptured?.Invoke(state);
                AppLogger.LogDebug("[ControllerIntegration] Input polled successfully.");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("[ControllerIntegration] Failed to poll input.", ex);
            }
        }
    }
}
