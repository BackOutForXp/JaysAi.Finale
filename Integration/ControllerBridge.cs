//monarch v2.1
using System;
using JaysAi.Finale.Input;

namespace JaysAi.Integration
{
    public class ControllerBridge
    {
        private readonly StickAssist stickAssist;
        private readonly ControllerState controllerState;

        public ControllerBridge(StickAssist assist, ControllerState state)
        {
            stickAssist = assist;
            controllerState = state;
        }

        public void UpdateStickAim(float deltaX, float deltaY)
        {
            var output = stickAssist.Calculate(deltaX, deltaY);
            SendAnalogOutput(output.X, output.Y);
        }

        public void SyncInputs()
        {
            if (controllerState.A) Press("A");
            if (controllerState.B) Press("B");
            if (controllerState.X) Press("X");
            if (controllerState.Y) Press("Y");
            // Add more if needed...
        }

        private void Press(string button)
        {
            // Placeholder for ZenStudio/TitanTwo signal send
            Console.WriteLine($"[Bridge] Simulated press: {button}");
        }

        private void SendAnalogOutput(float x, float y)
        {
            // Placeholder for analog stick command injection
            Console.WriteLine($"[Bridge] Analog Output -> X: {x:F2}, Y: {y:F2}");
        }
    }
}
