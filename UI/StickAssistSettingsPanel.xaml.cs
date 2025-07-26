// neural v3.0
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.UI.Settings
{
    public partial class StickAssistPanel : UserControl
    {
        public StickAssistPanel()
        {
            InitializeComponent();
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            DeadzoneSlider.Value = SettingsManager.Instance.StickDeadzone;
            DeadzoneValue.Text = DeadzoneSlider.Value.ToString("F2");
            ProportionalInput.Text = SettingsManager.Instance.PID_P.ToString(CultureInfo.InvariantCulture);
            IntegralInput.Text = SettingsManager.Instance.PID_I.ToString(CultureInfo.InvariantCulture);
            DerivativeInput.Text = SettingsManager.Instance.PID_D.ToString(CultureInfo.InvariantCulture);
        }

        private void DeadzoneSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DeadzoneValue.Text = e.NewValue.ToString("F2");
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (float.TryParse(ProportionalInput.Text, out float p) &&
                float.TryParse(IntegralInput.Text, out float i) &&
                float.TryParse(DerivativeInput.Text, out float d))
            {
                SettingsManager.Instance.StickDeadzone = (float)DeadzoneSlider.Value;
                SettingsManager.Instance.PID_P = p;
                SettingsManager.Instance.PID_I = i;
                SettingsManager.Instance.PID_D = d;

                MessageBox.Show("Stick Assist settings applied.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Invalid PID values.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

