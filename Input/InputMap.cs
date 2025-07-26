// neural v3.0
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.SystemLogic.Logging;
using JaysAi.Finale.Utility;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Input
{
    public class InputMap
    {
        private readonly Dictionary<string, Func<ControllerInputState, bool>> _mappings;

        public InputMap()
        {
            _mappings = new Dictionary<string, Func<ControllerInputState, bool>>(StringComparer.OrdinalIgnoreCase);
        }

        public void AddMapping(string action, Func<ControllerInputState, bool> predicate)
        {
            if (string.IsNullOrWhiteSpace(action))
            {
                Logger.Warn("InputMap: Attempted to add null or empty action.");
                return;
            }

            _mappings[action] = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public bool TryGetMappedAction(string action, ControllerInputState inputState, out bool result)
        {
            result = false;

            if (_mappings.TryGetValue(action, out var predicate))
            {
                result = predicate.Invoke(inputState);
                return true;
            }

            Logger.Trace($"InputMap: No mapping found for action '{action}'.");
            return false;
        }

        public IEnumerable<string> GetMappedActions()
        {
            return _mappings.Keys;
        }

        public void ClearMappings()
        {
            _mappings.Clear();
            Logger.Trace("InputMap: Cleared all mappings.");
        }
    }
}
