// File: MainWindow.xaml.cs
using JaysAi.Finale.Core;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using System.Windows;

namespace JaysAi.Finale
{
    public partial class MainWindow : Window
    {
        private readonly AppSettings _settings;

        public MainWindow()
        {
            InitializeComponent();

            _settings = SettingsManager<AppSettings>.Instance.Settings;

            // Set initial checkbox states based on saved config
            EspCheckbox.IsChecked = _settings.EnableESP;
            AimAssistCheckbox.IsChecked = _settings.EnableAimAssist;
            StickAssistCheckbox.IsChecked = _settings.EnableStickAssist;
            StealthCheckbox.IsChecked = _settings.EnableStealthMode;
            BoneEspCheckbox.IsChecked = _settings.EnableBoneESP;

            // Hook up events
            EspCheckbox.Checked += (s, e) => _settings.EnableESP = true;
            EspCheckbox.Unchecked += (s, e) => _settings.EnableESP = false;

            AimAssistCheckbox.Checked += (s, e) => _settings.EnableAimAssist = true;
            AimAssistCheckbox.Unchecked += (s, e) => _settings.EnableAimAssist = false;

            StickAssistCheckbox.Checked += (s, e) => _settings.EnableStickAssist = true;
            StickAssistCheckbox.Unchecked += (s, e) => _settings.EnableStickAssist = false;

            StealthCheckbox.Checked += (s, e) => _settings.EnableStealthMode = true;
            StealthCheckbox.Unchecked += (s, e) => _settings.EnableStealthMode = false;

            BoneEspCheckbox.Checked += (s, e) => _settings.EnableBoneESP = true;
            BoneEspCheckbox.Unchecked += (s, e) => _settings.EnableBoneESP = false;
        }
    }
}
