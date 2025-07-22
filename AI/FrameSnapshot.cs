//heavenly v3.0.0 – Frame Snapshot Structure
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Input;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class FrameSnapshot
    {
        public DateTime Timestamp { get; set; }
        public List<TrackedTarget> Targets { get; set; } = new();
        public ControllerInputState InputState { get; set; }
        public float PlayerYaw { get; set; }
        public float PlayerPitch { get; set; }
        public float PlayerFov { get; set; }

        public FrameSnapshot Clone()
        {
            return new FrameSnapshot
            {
                Timestamp = this.Timestamp,
                PlayerYaw = this.PlayerYaw,
                PlayerPitch = this.PlayerPitch,
                PlayerFov = this.PlayerFov,
                InputState = this.InputState?.Clone(),
                Targets = new List<TrackedTarget>(this.Targets)
            };
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss.fff}] Targets: {Targets.Count}, Yaw: {PlayerYaw:F2}, Pitch: {PlayerPitch:F2}";
        }
    }
}
