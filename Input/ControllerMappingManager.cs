//neural v3.0
using System;
using System.Collections.Generic;
using JaysAi.Finale.Input.Models;

namespace JaysAi.Finale.Input
{
    public class ControllerMappingManager
    {
        private readonly Dictionary<string, InputMappingProfile> _profiles;
        private InputMappingProfile _activeProfile;

        public ControllerMappingManager()
        {
            _profiles = new Dictionary<string, InputMappingProfile>(StringComparer.OrdinalIgnoreCase);
            _activeProfile = InputMappingProfile.Default;
        }

        public void RegisterProfile(string name, InputMappingProfile profile)
        {
            if (string.IsNullOrWhiteSpace(name) || profile == null)
                throw new ArgumentException("Invalid profile registration.");

            _profiles[name] = profile;
        }

        public bool TrySetActiveProfile(string name)
        {
            if (_profiles.TryGetValue(name, out var profile))
            {
                _activeProfile = profile;
                return true;
            }

            return false;
        }

        public MappedInput MapInput(ControllerInputState state)
        {
            return _activeProfile?.Map(state) ?? MappedInput.Empty;
        }

        public IReadOnlyDictionary<string, InputMappingProfile> GetProfiles() => _profiles;
    }
}
