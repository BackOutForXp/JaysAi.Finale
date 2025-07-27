using System;
using System.Windows;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Loader;

namespace JaysAi.Finale.UI
{
    public partial class LoaderGUI : Window
    {
        private AppSettings _settings => LoaderBootstrap.Settings;

        public LoaderGUI()
        {
            InitializeComponent();

            Loaded += OnLoaded;
            Closing += OnClosing;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoaderBootstrap.Initialize();

            // Bind checkboxes to AppSettings
            EspCheckbox.IsChecked = _settings.EnableESP;
            AimAssistCheckbox.IsChecked = _settings.EnableAimAssist;
            StickAssistCheckbox.IsChecked = _settings.EnableStickAssist;

            // Event handlers to live update AppSettings
            EspCheckbox.Checked += (_, _) => _settings.EnableESP = true;
            EspCheckbox.Unchecked += (_, _) => _settings.EnableESP = false;

            AimAssistCheckbox.Checked += (_, _) => _settings.EnableAimAssist = true;
            AimAssistCheckbox.Unchecked += (_, _) => _settings.EnableAimAssist = false;

            StickAssistCheckbox.Checked += (_, _) => _settings.EnableStickAssist = true;
            StickAssistCheckbox.Unchecked += (_, _) => _settings.EnableStickAssist = false;
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoaderBootstrap.Shutdown();
        }
    }
}
