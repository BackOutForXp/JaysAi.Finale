//monarch v2.1 – Real-time controller state model
namespace JaysAi.Finale.Input
{
    public class ControllerState
    {
        public float LeftStickX { get; set; }
        public float LeftStickY { get; set; }
        public float RightStickX { get; set; }
        public float RightStickY { get; set; }

        public bool A { get; set; }
        public bool B { get; set; }
        public bool X { get; set; }
        public bool Y { get; set; }

        public bool DpadUp { get; set; }
        public bool DpadDown { get; set; }
        public bool DpadLeft { get; set; }
        public bool DpadRight { get; set; }

        public bool LeftBumper { get; set; }
        public bool RightBumper { get; set; }

        public bool LeftTriggerPressed { get; set; }
        public bool RightTriggerPressed { get; set; }

        public bool Start { get; set; }
        public bool Select { get; set; }

        public bool IsConnected { get; set; }
    }
}
