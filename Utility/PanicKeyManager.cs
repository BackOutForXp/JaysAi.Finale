using System.Windows.Input;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Loader;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Utility
{
    public class PanicKeyManager
    {
        private readonly AppSettings _settings;
        private readonly KeybindWatcher _keybindWatcher;

        public PanicKeyManager(AppSettings settings, KeybindWatcher watcher)
        {
            _settings = settings;
            _keybindWatcher = watcher;
        }

        public void Register()
        {
            if (!_settings.KeybindsEnabled || string.IsNullOrWhiteSpace(_settings.PanicKey))
                return;

            if (Enum.TryParse(_settings.PanicKey, out Key key))
            {
                _keybindWatcher.Bind(key, TriggerPanic);
            }
        }

        private void TriggerPanic()
        {
            LoaderBootstrap.Shutdown();

            // Optional: Block input for 2 seconds
            InputBlocker.Block();
            System.Threading.Thread.Sleep(2000);
            InputBlocker.Unblock();

            System.Environment.Exit(0);
        }
    }
}
