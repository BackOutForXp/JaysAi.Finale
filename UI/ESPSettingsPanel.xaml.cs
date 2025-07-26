// neural v3.0
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Overlay.Panels.Components;

namespace JaysAi.Finale.Overlay.Panels
{
    public partial class ESPSettingsPanel : UserControl
    {
        public ESPSettingsPanel()
        {
            InitializeComponent();
            Loaded += ESPSettingsPanel_Loaded;
        }

        private void ESPSettingsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleESP.IsChecked = UserSettings.Instance.ESPEnabled;
            ToggleBox.IsChecked = UserSettings.Instance.BoxESP;
            ToggleHealthBar.IsChecked = UserSettings.Instance.HealthBarESP;
        }

        private void ToggleESP_Checked(object sender, RoutedEventArgs e)
        {
            UserSettings.Instance.ESPEnabled = ToggleESP.IsChecked == true;
        }

        private void ToggleBox_Checked(object sender, RoutedEventArgs e)
        {
            UserSettings.Instance.BoxESP = ToggleBox.IsChecked == true;
        }

        private void ToggleHealthBar_Checked(object sender, RoutedEventArgs e)
        {
            UserSettings.Instance.HealthBarESP = ToggleHealthBar.IsChecked == true;
        }
    }
}
