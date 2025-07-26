// neural v3.0
using System.Windows.Controls;

namespace JaysAi.Finale.UI.Panels
{
    public partial class CrosshairSettingsPanel : UserControl
    {
        public CrosshairSettingsPanel()
        {
            InitializeComponent();
            DataContext = new CrosshairSettingsViewModel();
        }
    }
}
