// File: UI/ESPSettingsPanel.xaml.cs
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.UI
{
    public partial class ESPSettingsPanel : UserControl
    {
        private readonly SettingsManager<AppSettings> _settings;

        public ESPSettingsPanel()
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
                _settings.Current.ESP.EnemyColor = System.Drawing.Color.FromArgb(
                    selected.A, selected.R, selected.G, selected.B);
                _settings.Save();
            }
        }
    }
}
