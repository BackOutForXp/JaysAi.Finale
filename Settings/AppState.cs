//neural v3.0
using System;

namespace JaysAi.Finale.Settings
{
    public sealed class AppState
    {
        private static readonly Lazy<AppState> _instance = new(() => new AppState());
        public static AppState Instance => _instance.Value;

        public bool IsInitialized { get; private set; }
        public bool IsAuthenticated { get; set; }
        public bool IsOverlayVisible { get; set; }
        public string CurrentProfile { get; set; } = "default";
        public DateTime LaunchTimestamp { get; }

        private AppState()
        {
            LaunchTimestamp = DateTime.UtcNow;
        }

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void Reset()
        {
            IsAuthenticated = false;
            IsOverlayVisible = false;
            CurrentProfile = "default";
            IsInitialized = false;
        }
    }
}
