// neural v3.0
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.SystemLogic.Events;
using JaysAi.Finale.SystemLogic.Logging;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Input
{
    public sealed class InputInterceptor
    {
        private readonly Func<ControllerInputState, bool> _interceptionLogic;

        public event EventHandler<InterceptedInputEventArgs>? InputIntercepted;

        public InputInterceptor(Func<ControllerInputState, bool> interceptionLogic)
        {
            _interceptionLogic = interceptionLogic ?? throw new ArgumentNullException(nameof(interceptionLogic));
        }

        public bool TryIntercept(ControllerInputState inputState)
        {
            if (_interceptionLogic.Invoke(inputState))
            {
                OnInputIntercepted(inputState);
                Logger.Debug("Input intercepted.");
                return true;
            }

            return false;
        }

        private void OnInputIntercepted(ControllerInputState input)
        {
            InputIntercepted?.Invoke(this, new InterceptedInputEventArgs(input));
        }
    }

    public class InterceptedInputEventArgs : EventArgs
    {
        public ControllerInputState InputState { get; }

        public InterceptedInputEventArgs(ControllerInputState inputState)
        {
            InputState = inputState;
        }
    }
}
