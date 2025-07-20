//monarch v2.1 – Global runtime status tracker
using System;

namespace JaysAi.Finale.SystemLogic
{
    public static class LoaderState
    {
        public static bool IsInitialized { get; set; } = false;
        public static bool IsRunning { get; set; } = false;
        public static bool IsGuiVisible { get; set; } = true;
        public static bool IsInStealthMode { get; set; } = false;

        public static DateTime LastStartTime { get; private set; }
        public static DateTime LastUpdateTime { get; private set; }

        public static void MarkStarted()
        {
            IsInitialized = true;
            IsRunning = true;
            LastStartTime = DateTime.Now;
        }

        public static void MarkUpdated()
        {
            LastUpdateTime = DateTime.Now;
        }

        public static void ToggleGui()
        {
            IsGuiVisible = !IsGuiVisible;
        }

        public static void EnterStealthMode()
        {
            IsInStealthMode = true;
            IsGuiVisible = false;
        }

        public static void ExitStealthMode()
        {
            IsInStealthMode = false;
            IsGuiVisible = true;
        }
    }
}
