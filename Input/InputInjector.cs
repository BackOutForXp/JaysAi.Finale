// neural v3.0
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.SystemLogic.Handlers;
using JaysAi.Finale.SystemLogic.Logging;
using JaysAi.Finale.Utility;
using System;

namespace JaysAi.Finale.Input
{
    public sealed class InputInjector
    {
        private readonly IInputDispatcher _dispatcher;
        private readonly IInputValidator _validator;

        public InputInjector(IInputDispatcher dispatcher, IInputValidator validator)
        {
            _dispatcher = dispatcher;
            _validator = validator;
        }

        public bool Inject(ControllerInputState state)
        {
            if (!_validator.Validate(state))
            {
                Logger.Warn("Invalid input rejected by validator.");
                return false;
            }

            try
            {
                _dispatcher.Dispatch(state);
                Logger.Trace("Input successfully dispatched.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Inject failed: " + ex.Message);
                return false;
            }
        }

        public void ForceInject(ControllerInputState state)
        {
            try
            {
                _dispatcher.Dispatch(state);
                Logger.Trace("Force-injected input without validation.");
            }
            catch (Exception ex)
            {
                Logger.Error("ForceInject failed: " + ex.Message);
            }
        }
    }
}
