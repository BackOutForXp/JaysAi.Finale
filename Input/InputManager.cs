//monarch v2.1 – Centralized Input Manager

using global::System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public static class InputManager
    {
        public static bool IsKeyPressed(Key key)
        {
            return Keyboard.IsKeyDown(key);
        }

        public static bool IsLeftMousePressed()
        {
            return Mouse.LeftButton == MouseButtonState.Pressed;
        }

        public static bool IsRightMousePressed()
        {
            return Mouse.RightButton == MouseButtonState.Pressed;
        }
    }
}
