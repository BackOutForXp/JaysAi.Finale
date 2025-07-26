// neural v3.0
using System;
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public sealed class ControllerState
    {
        public Vector2 LeftStick { get; private set; }
        public Vector2 RightStick { get; private set; }
        public float LeftTrigger { get; private set; }
        public float RightTrigger { get; private set; }
        public ControllerButtons Buttons { get; private set; }

        public float DeadzoneThreshold { get; set; } = 0.1f;

        public void Update(ControllerInputState input)
        {
            LeftStick = ApplyDeadzone(input.LeftStick);
            RightStick = ApplyDeadzone(input.RightStick);
            LeftTrigger = input.LeftTrigger;
            RightTrigger = input.RightTrigger;
            Buttons = input.Buttons;
        }

        private Vector2 ApplyDeadzone(Vector2 raw)
        {
            var length = raw.Length();
            return length < DeadzoneThreshold ? Vector2.Zero : raw;
        }

        public bool IsButtonPressed(ControllerButtons button)
        {
            return (Buttons & button) != 0;
        }

        public override string ToString()
        {
            return $"LStick:{LeftStick} RStick:{RightStick} LT:{LeftTrigger} RT:{RightTrigger} Buttons:{Buttons}";
        }
    }

    [Flags]
    public enum ControllerButtons
    {
        None = 0,
        A = 1 << 0,
        B = 1 << 1,
        X = 1 << 2,
        Y = 1 << 3,
        LB = 1 << 4,
        RB = 1 << 5,
        Back = 1 << 6,
        Start = 1 << 7,
        LS = 1 << 8,
        RS = 1 << 9,
        DPadUp = 1 << 10,
        DPadDown = 1 << 11,
        DPadLeft = 1 << 12,
        DPadRight = 1 << 13
    }
}
