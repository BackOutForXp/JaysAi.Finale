// Neural v3.0
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.UI
{
    public partial class AimAssistSettingsPanel : UserControl
    {
        private readonly UserSettings _userSettings;

        public AimAssistSettingsPanel()
        {
            InitializeComponent();
            _userSettings = SettingsManager.Instance.CurrentUserSettings;
            LoadSettings();
            RegisterEvents();
        }

        private void LoadSettings()
        {
            EnableAimAssistCheckBox.IsChecked = _userSettings.AimAssistEnabled;
            StrengthSlider.Value = _userSettings.AimAssistStrength;
            FovSlider.Value = _userSettings.AimAssistFov;
            SmoothingSlider.Value = _userSettings.AimAssistSmoothing;
        }

        private void RegisterEvents()
        {
            EnableAimAssistCheckBox.Checked += (s, e) => UpdateSetting(true);
            EnableAimAssistCheckBox.Unchecked += (s, e) => UpdateSetting(false);

            StrengthSlider.ValueChanged += (s, e) =>
            {
                _userSettings.AimAssistStrength = (int)StrengthSlider.Value;
                SettingsManager.Instance.Save();
            };

            FovSlider.ValueChanged += (s, e) =>
            {
                _userSettings.AimAssistFov = (int)FovSlider.Value;
                SettingsManager.Instance.Save();
            };

            SmoothingSlider.ValueChanged += (s, e) =>
            {
                _userSettings.AimAssistSmoothing = (int)SmoothingSlider.Value;
                SettingsManager.Instance.Save();
            };
        }

        private void UpdateSetting(bool enabled)
        {
            _userSettings.AimAssistEnabled = enabled;
            SettingsManager.Instance.Save();
        }
    }
}
