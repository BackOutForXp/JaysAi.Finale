// File: UI/CrosshairSettingsPanel.xaml.cs
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;
using Microsoft.Win32;
using System.Windows.Media;

namespace JaysAi.Finale.UI
{
    public partial class CrosshairSettingsPanel : UserControl
    {
        private readonly SettingsManager<AppSettings> _settings;

        public CrosshairSettingsPanel()
        {
            InitializeComponent();

            _settings = SettingsManager<AppSettings>.Instance;
            DataContext = _settings.Current;
        }

        private void OnPickColorClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var selected = dialog.Color;
                _settings.Current.Crosshair.Color = System.Drawing.Color.FromArgb(
                    selected.A, selected.R, selected.G, selected.B);
                _settings.Save();
            }
        }
    }
}
