// neural v3.0
using System;

namespace JaysAi.Finale.Input.Events
{
    public class InputStateChangedEvent : EventArgs
    {
        public int ControllerId { get; }
        public ControllerInputState PreviousState { get; }
        public ControllerInputState CurrentState { get; }
        public DateTime Timestamp { get; }

        public InputStateChangedEvent(int controllerId, ControllerInputState previous, ControllerInputState current)
        {
            ControllerId = controllerId;
            PreviousState = previous;
            CurrentState = current;
            Timestamp = DateTime.UtcNow;
        }
    }
}
