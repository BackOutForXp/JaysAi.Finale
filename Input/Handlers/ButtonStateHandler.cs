// neural v3.0
using System.Collections.Generic;
using JaysAi.Finale.Input.Structs;

namespace JaysAi.Finale.Input.Handlers
{
    public class ButtonStateHandler
    {
        private readonly Dictionary<int, ButtonState> _previousStates = new();

        public bool HasButtonStateChanged(int controllerId, ControllerInputState newState)
        {
            if (!_previousStates.TryGetValue(controllerId, out var previousState))
            {
                _previousStates[controllerId] = newState.Buttons.Clone();
                return true;
            }

            bool changed = !previousState.Equals(newState.Buttons);
            if (changed)
                _previousStates[controllerId] = newState.Buttons.Clone();

            return changed;
        }
    }
}
