// File: UI/StickAssistSettingsPanel.xaml.cs
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.UI
{
    public partial class StickAssistSettingsPanel : UserControl
    {
        private readonly SettingsManager<AppSettings> _settings;

        public StickAssistSettingsPanel()
        {
            InitializeComponent();
            _settings = SettingsManager<AppSettings>.Instance;
            DataContext = _settings.Current;
        }
    }
}
