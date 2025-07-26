// neural v3.0
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Input.Mapping
{
    public class InputMappingProfile
    {
        private readonly Dictionary<string, Func<float, float>> _axisMappings;
        private readonly Dictionary<string, string> _buttonMappings;
        private readonly Dictionary<string, float> _deadzoneOverrides;

        public InputMappingProfile()
        {
            _axisMappings = new();
            _buttonMappings = new();
            _deadzoneOverrides = new();
        }

        public void MapButton(string source, string target)
        {
            _buttonMappings[source] = target;
        }

        public void MapAxis(string source, Func<float, float> transformation)
        {
            _axisMappings[source] = transformation;
        }

        public void SetDeadzone(string axisName, float threshold)
        {
            _deadzoneOverrides[axisName] = threshold;
        }

        public string ResolveButton(string source)
        {
            return _buttonMappings.TryGetValue(source, out var mapped)
                ? mapped
                : source;
        }

        public float ResolveAxis(string source, float inputValue)
        {
            if (_deadzoneOverrides.TryGetValue(source, out var threshold) && Math.Abs(inputValue) < threshold)
                return 0f;

            return _axisMappings.TryGetValue(source, out var func)
                ? func(inputValue)
                : inputValue;
        }

        public float GetDeadzone(string axisName)
        {
            return _deadzoneOverrides.TryGetValue(axisName, out var dz) ? dz : 0.05f;
        }
    }
}
