// neural v3.0
namespace JaysAi.Finale.SystemLogic
{
    public static class LoaderState
    {
        private static bool _isInitialized = false;
        private static bool _isInStealthMode = false;
        private static bool _isAuthenticated = false;
        private static string? _currentProfileName = null;

        public static bool IsInitialized
        {
            get => _isInitialized;
            set => _isInitialized = value;
        }

        public static bool IsInStealthMode
        {
            get => _isInStealthMode;
            set => _isInStealthMode = value;
        }

        public static bool IsAuthenticated
        {
            get => _isAuthenticated;
            set => _isAuthenticated = value;
        }

        public static string? CurrentProfileName
        {
            get => _currentProfileName;
            set => _currentProfileName = value;
        }

        public static void Reset()
        {
            _isInitialized = false;
            _isInStealthMode = false;
            _isAuthenticated = false;
            _currentProfileName = null;
        }
    }
}
