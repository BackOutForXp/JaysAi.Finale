using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Core;

namespace JaysAi.Finale.UI
{
    public partial class AimAssistSettingsPanel : UserControl
    {
        private readonly AppSettings _settings;

        // 👇 Required for XAML
        public AimAssistSettingsPanel() : this(new AppSettings()) { }

        public AimAssistSettingsPanel(AppSettings settings)
        {
            InitializeComponent();
            _settings = settings;

            SmoothingSlider.Value = _settings.AimSmoothing;
            BulletSpeedSlider.Value = _settings.BulletSpeed;
            SnapDistanceSlider.Value = _settings.SnapDistance;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            _settings.AimSmoothing = SmoothingSlider.Value;
            _settings.BulletSpeed = BulletSpeedSlider.Value;
            _settings.SnapDistance = SnapDistanceSlider.Value;

            var manager = new SettingsManager<AppSettings>("config.json");
            manager.Save();

            MessageBox.Show("AimAssist settings saved.");
        }
    }
}
