using JaysAi.Finale.Loader;
using JaysAi.Finale.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JaysAi.Finale.UI
{
    public partial class KeybindSettingsPanel : UserControl
    {
        private AppSettings _settings => LoaderBootstrap.Settings;

        public KeybindSettingsPanel()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Populate all key dropdowns with available Keys
            var keys = Enum.GetValues(typeof(Key)).Cast<Key>()
                           .Where(k => k >= Key.A && k <= Key.Z || k >= Key.D0 && k <= Key.D9 || k == Key.LeftShift || k == Key.RightShift || k == Key.LeftCtrl || k == Key.RightCtrl)
                           .Distinct()
                           .ToList();

            EspKeyDropdown.ItemsSource = keys;
            AimKeyDropdown.ItemsSource = keys;
            PanicKeyDropdown.ItemsSource = keys;

            // Set current selections from settings
            EspKeyDropdown.SelectedItem = GetKeyFromString(_settings.ESP_ToggleKey);
            AimKeyDropdown.SelectedItem = GetKeyFromString(_settings.Aim_HoldKey);
            PanicKeyDropdown.SelectedItem = GetKeyFromString(_settings.PanicKey);

            AllowKeybindsCheckbox.IsChecked = _settings.KeybindsEnabled;

            // Bind events
            EspKeyDropdown.SelectionChanged += (_, _) =>
                _settings.ESP_ToggleKey = EspKeyDropdown.SelectedItem?.ToString();

            AimKeyDropdown.SelectionChanged += (_, _) =>
                _settings.Aim_HoldKey = AimKeyDropdown.SelectedItem?.ToString()