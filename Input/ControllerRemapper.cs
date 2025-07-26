//neural v3.0
using System;
using JaysAi.Finale.Input.Models;

namespace JaysAi.Finale.Input
{
    public class ControllerRemapper
    {
        private readonly InputMappingProfile _profile;

        public ControllerRemapper(InputMappingProfile profile)
        {
            _profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        public MappedInput Remap(ControllerInputState rawState)
        {
            if (rawState == null)
                return MappedInput.Empty;

            return _profile.Map(rawState);
        }

        public static ControllerRemapper WithDefaultProfile()
        {
            return new ControllerRemapper(InputMappingProfile.Default);
        }

        public static ControllerRemapper FromCustom(Action<InputMappingProfile> configAction)
        {
            var profile = new InputMappingProfile();
            configAction?.Invoke(profile);
            return new ControllerRemapper(profile);
        }
    }
}
