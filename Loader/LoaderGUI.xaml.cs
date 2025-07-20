// File: Loader/LoaderGUI.xaml.cs
using JaysAi.Finale.Settings;
using JaysAi.Finale.UI;
using System.Windows;
using System.Windows.Controls;

namespace JaysAi.Finale.Loader
{
    public partial class LoaderGUI : Window
    {
        private readonly AppSettings _settings;

        public LoaderGUI()
        {
            InitializeComponent();
            _settings = SettingsManager<AppSettings>.Settings;

            InitializeCheckboxes();
            AttachCheckboxEvents();
        }

        private void InitializeCheckboxes()
        {
            EspCheckbox.IsChecked = _settings.EnableESP;
            AimAssistCheckbox.IsChecked = _settings.EnableAimAssist;
            StickAssistCheckbox.IsChecked = _settings.EnableStickAssist;
            StealthCheckbox.IsChecked = _settings.EnableStealth;
            BoneEspCheckbox.IsChecked = _settings.EnableBoneESP;
        }

        private void AttachCheckboxEvents()
        {
            EspCheckbox.Checked += (s, e) => UpdateSetting(x => x.EnableESP = true);
            EspCheckbox.Unchecked += (s, e) => UpdateSetting(x => x.EnableESP = false);

            AimAssistCheckbox.Checked += (s, e) => UpdateSetting(x => x.EnableAimAssist = true);
            AimAssistCheckbox.Unchecked += (s, e) => UpdateSetting(x => x.EnableAimAssist = false);

            StickAssistCheckbox.Checked += (s, e) => UpdateSetting(x => x.EnableStickAssist = true);
            StickAssistCheckbox.Unchecked += (s, e) => UpdateSetting(x => x.EnableStickAssist = false);

            StealthCheckbox.Checked += (s, e) => UpdateSetting(x => x.EnableStealth = true);
            StealthCheckbox.Unchecked += (s, e) => UpdateSetting(x => x.EnableStealth = false);

            BoneEspCheckbox.Checked += (s, e) => UpdateSetting(x => x.EnableBoneESP = true);
            BoneEspCheckbox.Unchecked += (s, e) => UpdateSetting(x => x.EnableBoneESP = false);
        }

        private void UpdateSetting(System.Action<AppSettings> update)
        {
            update(_settings);
            SettingsManager<AppSettings>.Save();
        }
    }
}
