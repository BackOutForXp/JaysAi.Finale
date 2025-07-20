//monarch v2.0
using JaysAi.Utility;
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public static class InputInterceptor
    {
        public static Key ToggleSnapKey = Key.F3;
        public static bool SnapEnabled = true;

        public static void HandleInput()
        {
            if (Keyboard.IsKeyDown(ToggleSnapKey))
            {
                SnapEnabled = !SnapEnabled;
                Logger.Log("Snap Assist toggled: " + (SnapEnabled ? "ON" : "OFF"));
                Thread.Sleep(150); // prevent key spam
            }
        }
    }
}
