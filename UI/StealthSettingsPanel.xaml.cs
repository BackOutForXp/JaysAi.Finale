// Neural v3.1 — StealthSettingsPanel.xaml.cs
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.UI
{
    public partial class StealthSettingsPanel : UserControl
    {
        public StealthSettingsPanel()
        {
            InitializeComponent();
            StealthToggle.IsChecked = UserSettings.Current.Get("EnableStealth", false);
        }

        private void StealthToggle_Checked(object sender, RoutedEventArgs e)
        {
            UserSettings.Current.Set("EnableStealth", true);
        }

        private void StealthToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            UserSettings.Current.Set("EnableStealth", false);
        }
    }
}
