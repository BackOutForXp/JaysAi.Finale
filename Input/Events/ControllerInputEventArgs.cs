// Neural v3.0 — ControllerInputEventArgs.cs
using JaysAi.Finale.Input;
using JaysAi.Finale.Input.Enums;
using System;

namespace JaysAi.Finale.Input.Events
{
    public class ControllerInputEventArgs : EventArgs
    {
        public ControllerInputState State { get; }
        public InputDeviceType DeviceType { get; }

        public ControllerInputEventArgs(ControllerInputState state, InputDeviceType deviceType)
        {
            State = state;
            DeviceType = deviceType;
        }
    }
}
