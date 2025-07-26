// Neural v3.0 — IInputSource.cs
using JaysAi.Finale.Input.Enums;

namespace JaysAi.Finale.Input
{
    public interface IInputSource
    {
        bool IsConnected { get; }
        InputDeviceType DeviceType { get; }

        void Initialize();
        void UpdateInputState();
        ControllerInputState GetCurrentState();
    }
}

