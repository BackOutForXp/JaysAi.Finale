//monarch v2.1
namespace JaysAi.Finale.Input
{
    public class ControllerInputState
    {
        public float LeftStickX { get; set; }
        public float LeftStickY { get; set; }
        public float RightStickX { get; set; }
        public float RightStickY { get; set; }
        public float LT { get; set; }
        public float RT { get; set; }

        public bool ButtonA { get; set; }
        public bool ButtonB { get; set; }
        public bool ButtonX { get; set; }
        public bool ButtonY { get; set; }

        public bool LB { get; set; }
        public bool RB { get; set; }
        public bool DPadUp { get; set; }
        public bool DPadDown { get; set; }
        public bool DPadLeft { get; set; }
        public bool DPadRight { get; set; }

        public void Reset()
        {
            LeftStickX = LeftStickY = RightStickX = RightStickY = 0;
            LT = RT = 0;
            ButtonA = ButtonB = ButtonX = ButtonY = false;
            LB = RB = DPadUp = DPadDown = DPadLeft = DPadRight = false;
        }
    }
}
