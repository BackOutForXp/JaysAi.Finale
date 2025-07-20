//monarch v2.1
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

        public bool A { get; set; }
        public bool B { get; set; }
        public bool X { get; set; }
        public bool Y { get; set; }

        public bool LB { get; set; }
        public bool RB { get; set; }

        public bool DPadUp { get; set; }
        public bool DPadDown { get; set; }
        public bool DPadLeft { get; set; }
        public bool DPadRight { get; set; }

        public bool Start { get; set; }
        public bool Select { get; set; }

        public void Reset()
        {
            LeftStickX = LeftStickY = RightStickX = RightStickY = 0f;
            LeftTrigger = RightTrigger = 0f;
            A = B = X = Y = LB = RB = false;
            DPadUp = DPadDown = DPadLeft = DPadRight = false;
            Start = Select = false;
        }
    }
}
