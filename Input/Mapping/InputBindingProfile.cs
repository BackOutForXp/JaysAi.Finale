// neural v3.0
using System.Collections.Generic;

namespace JaysAi.Finale.Input.Mapping
{
    public class InputBindingProfile
    {
        public Dictionary<string, string> ButtonBindings { get; } = new();
        public Dictionary<string, string> AxisBindings { get; } = new();

        public void BindButton(string physicalInput, string virtualAction)
        {
            ButtonBindings[physicalInput] = virtualAction;
        }

        public void BindAxis(string physicalAxis, string virtualAxis)
        {
            AxisBindings[physicalAxis] = virtualAxis;
        }

        public string GetBoundButton(string physicalInput)
        {
            return ButtonBindings.TryGetValue(physicalInput, out var binding)
                ? binding
                : physicalInput;
        }

        public string GetBoundAxis(string physicalAxis)
        {
            return AxisBindings.TryGetValue(physicalAxis, out var binding)
                ? binding
                : physicalAxis;
        }

        public void ClearBindings()
        {
            ButtonBindings.Clear();
            AxisBindings.Clear();
        }
    }
}
