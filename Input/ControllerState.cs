//heavenly v3.0 – Controller Snapshot State
using System;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public class ControllerState
    {
        public float LeftStickX { get; set; }
        public float LeftStickY { get; set; }
        public float RightStickX { get; set; }
        public float RightStickY { get; set; }

        public float LeftTrigger { get; set; }
        public float RightTrigger { get; set; }

        public bool ButtonA { get; set; }
        public bool ButtonB { get; set; }
        public bool ButtonX { get; set; }
        public bool ButtonY { get; set; }

        public bool DPadUp { get; set; }
        public bool DPadDown { get; set; }
        public bool DPadLeft { get; set; }
        public bool DPadRight { get; set; }

        public bool LeftBumper { get; set; }
        public bool RightBumper { get; set; }

        public bool LeftStickClick { get; set; }
        public bool RightStickClick { get; set; }

        public bool Start { get; set; }
        public bool Back { get; set; }
        public bool Guide { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public Vector2 GetLeftStick() => new(LeftStickX, LeftStickY);
        public Vector2 GetRightStick() => new(RightStickX, RightStickY);

        public ControllerState Clone()
        {
            return (ControllerState)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"LS:({LeftStickX:F2},{LeftStickY:F2}) RS:({RightStickX:F2},{RightStickY:F2}) RT:{RightTrigger:F2} LT:{LeftTrigger:F2}";
        }
    }
}
