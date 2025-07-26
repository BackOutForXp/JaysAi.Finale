// neural v3.0
using JaysAi.Finale.Input.Handlers;
using JaysAi.Finale.Input.Interfaces;
using JaysAi.Finale.Input.Devices;

namespace JaysAi.Finale.Input
{
    public static class InputHandlerFactory
    {
        public static IInputHandler CreateDefault()
        {
            // Default to keyboard input if no controller is available
            if (ControllerDetector.IsControllerConnected())
                return new ControllerInputHandler();

            return new KeyboardInputHandler();
        }

        public static IInputHandler CreateSpecific(string deviceType)
        {
            return deviceType.ToLower() switch
            {
                "controller" => new ControllerInputHandler(),
                "keyboard" => new KeyboardInputHandler(),
                "virtual" => new VirtualInputHandler(),
                _ => CreateDefault(),
            };
        }
    }
}
