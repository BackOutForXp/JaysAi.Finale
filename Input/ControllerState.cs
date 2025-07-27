// Neural v3.1
using System.Numerics;

namespace JaysAi.Finale.Input
{
    public class ControllerState
    {
        public Vector2 LeftStick { get; set; }
        public Vector2 RightStick { get; set; }

        public float LeftTrigger { get; set; }
        public float RightTrigger { get; set; }

        public bool A { get; set; }
        public bool B { get; set; }
        public bool X { get; set; }
        public bool Y { get; set; }

        public bool DPadUp { get; set; }
        public bool DPadDown { get; set; }
        public bool DPadLeft { get; set; }
        public bool DPadRight { get; set; }

        public bool Start { get; set; }
        public bool Back { get; set; }

        public bool LeftBumper { get; set; }
        public bool RightBumper { get; set; }

        public bool LeftStickPressed { get; set; }
        public bool RightStickPressed { get; set; }

        public bool IsConnected { get; set; } = false;

        public void Reset()
        {
            LeftStick = Vector2.Zero;
            RightStick = Vector2.Zero;
            LeftTrigger = 0;
            RightTrigger = 0;

            A = B = X = Y = false;
            DPadUp = DPadDown = DPadLeft = DPadRight = false;
            Start = Back = false;
            LeftBumper = RightBumper = false;
            LeftStickPressed = RightStickPressed = false;
            IsConnected = false;
        }
    }
}
