//neural v3.0
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Modules.Stick;
using JaysAi.Finale.Structures;

namespace JaysAi.Finale.Modules
{
    public sealed class StickInputBridge
    {
        private readonly StickCalibrator _calibrator;
        private readonly DeadzoneFilter _deadzoneFilter;
        private readonly StickZoneProfile _profile;

        public StickInputBridge()
        {
            _calibrator = new StickCalibrator();
            _deadzoneFilter = new DeadzoneFilter();
            _profile = StickZoneProfileLoader.Default;
        }

        public StickInputData Process(ControllerInputState inputState)
        {
            var calibrated = _calibrator.ApplyCalibration(inputState.LeftStick, inputState.RightStick);
            var filtered = _deadzoneFilter.Apply(calibrated.Left, calibrated.Right);

            return new StickInputData
            {
                Left = filtered.Left,
                Right = filtered.Right,
                IsInsideSnapZone = _profile.IsInsideSnapZone(filtered.Right),
                Angle = MathHelper.CalculateAngle(filtered.Right),
                Magnitude = MathHelper.CalculateMagnitude(filtered.Right)
            };
        }

        public void SetDeadzone(float deadzone)
        {
            _deadzoneFilter.SetThreshold(deadzone);
        }

        public void SetProfile(StickZoneProfile profile)
        {
            if (profile != null)
                _profile.CopyFrom(profile);
        }
    }
}
