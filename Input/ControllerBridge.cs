//monarch v2.1 – Gamepad Input Bridge (Xbox/PS support)

using SharpDX.XInput;

namespace JaysAi.Finale.Input
{
    public static class ControllerBridge
    {
        private static Controller controller = new Controller(UserIndex.One);

        public static bool IsConnected => controller.IsConnected;

        public static Gamepad GetState()
        {
            if (!controller.IsConnected)
                return default;

            return controller.GetState().Gamepad;
        }

        public static bool IsButtonPressed(GamepadButtonFlags button)
        {
            if (!controller.IsConnected)
                return false;

            return (controller.GetState().Gamepad.Buttons & button) != 0;
        }
    }
}
