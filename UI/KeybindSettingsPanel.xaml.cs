// File: UI/KeybindSettingsPanel.xaml.cs
using JaysAi.Finale.Settings;
using System.Windows.Controls;

namespace JaysAi.Finale.UI
{
    public partial class KeybindSettingsPanel : UserControl
    {
        public KeybindSettingsPanel()
        {
            InitializeComponent();
            this.DataContext = SettingsManager<AppSettings>.Current;
        }
    }
}
