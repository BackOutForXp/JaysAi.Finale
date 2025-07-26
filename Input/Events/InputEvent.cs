// neural v3.0
using System;

namespace JaysAi.Finale.Input.Events
{
    public class InputEvent : EventArgs
    {
        public int DeviceId { get; }
        public string EventType { get; }
        public DateTime Timestamp { get; }

        public InputEvent(int deviceId, string eventType)
        {
            DeviceId = deviceId;
            EventType = eventType;
            Timestamp = DateTime.UtcNow;
        }
    }
}
