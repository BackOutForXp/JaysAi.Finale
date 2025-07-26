// neural v3.0
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Integration;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.Hooks
{
    /// <summary>
    /// Manages and initializes all low-level and high-level system hooks required for the loader.
    /// </summary>
    public class SystemHookManager
    {
        private readonly MouseHook _mouseHook;
        private readonly KeyboardHook _keyboardHook;
        private readonly ControllerHook _controllerHook;

        private bool _isInitialized;

        public SystemHookManager()
        {
            _mouseHook = new MouseHook();
            _keyboardHook = new KeyboardHook();
            _controllerHook = new ControllerHook();
        }

        public void InitializeHooks()
        {
            if (_isInitialized)
                return;

            try
            {
                _mouseHook.Hook();
                _keyboardHook.Hook();
                _controllerHook.Initialize();

                LogSystem.Info("System hooks successfully initialized.");
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error("Failed to initialize hooks: " + ex.Message);
            }
        }

        public void ShutdownHooks()
        {
            if (!_isInitialized)
                return;

            try
            {
                _mouseHook.Unhook();
                _keyboardHook.Unhook();
                _controllerHook.Shutdown();

                LogSystem.Info("System hooks successfully released.");
                _isInitialized = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error("Failed to shut down hooks: " + ex.Message);
            }
        }

        public bool AreHooksActive => _isInitialized;
    }
}
