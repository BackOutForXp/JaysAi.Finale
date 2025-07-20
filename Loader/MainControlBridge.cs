//monarch v2.0
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Loader
{
    public class MainControlBridge
    {
        private readonly UserSettings _settings;

        public MainControlBridge()
        {
            _settings = UserSettings.Load();
            ApplySettings();
        }

        private void ApplySettings()
        {
            FeatureToggleManager.EspEnabled = _settings.EspEnabled;
            FeatureToggleManager.AimAssistEnabled = _settings.AimAssistEnabled;
            FeatureToggleManager.SnapAssistEnabled = _settings.SnapAssistEnabled;
        }

        public void ToggleEsp()
        {
            FeatureToggleManager.ToggleEsp();
            LogManager.Log($"ESP toggled: {FeatureToggleManager.EspEnabled}");
        }

        public void ToggleAimAssist()
        {
            FeatureToggleManager.ToggleAimAssist();
            LogManager.Log($"Aim Assist toggled: {FeatureToggleManager.AimAssistEnabled}");
        }

        public void ToggleSnapAssist()
        {
            FeatureToggleManager.ToggleSnapAssist();
            LogManager.Log($"Snap Assist toggled: {FeatureToggleManager.SnapAssistEnabled}");
        }

        public void SaveSettings()
        {
            _settings.EspEnabled = FeatureToggleManager.EspEnabled;
            _settings.AimAssistEnabled = FeatureToggleManager.AimAssistEnabled;
            _settings.SnapAssistEnabled = FeatureToggleManager.SnapAssistEnabled;
            _settings.Save();
            LogManager.Log("Settings saved.");
        }
    }
}
