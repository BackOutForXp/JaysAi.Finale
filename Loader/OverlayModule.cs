using JaysAi.Finale.Settings;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.Loader
{
    public class OverlayModule : IModule
    {
        private readonly AppSettings _settings;
        private OverlayWindow _overlayWindow;

        public OverlayModule(AppSettings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            if (!_settings.EnableOverlay)
                return;

            _overlayWindow = new OverlayWindow(_settings);
            _overlayWindow.Show();
        }

        public void Shutdown()
        {
            _overlayWindow?.Close();
            _overlayWindow = null;
        }
    }
}
