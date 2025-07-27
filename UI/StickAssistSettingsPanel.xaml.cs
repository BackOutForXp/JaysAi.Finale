// Neural v3.1 — StickAssistSettingsPanel.xaml.cs
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.UI
{
    public partial class StickAssistSettingsPanel : UserControl
    {
        public StickAssistSettingsPanel()
        {
            InitializeComponent();
            DataContext = UserSettings.Instance;
        }

        private void StickAssistEnabled_Checked(object sender, RoutedEventArgs e)
        {
            UserSettings.Instance.Set("StickAssistEnabled", true);
        }

        private void StickAssistEnabled_Unchecked(object sender, RoutedEventArgs e)
        {
            UserSettings.Instance.Set("StickAssistEnabled", false);
        }

        private void StrengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsLoaded)
            {
                UserSettings.Instance.Set("StickAssistStrength", (float)e.NewValue);
            }
        }

        private void RadiusSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsLoaded)
            {
                UserSettings.Instance.Set("StickAssistFovRadius", (float)e.NewValue);
            }
        }
    }
}
