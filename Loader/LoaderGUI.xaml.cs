//monarch v2.1 – Loader GUI logic and live toggle handlers
using System.Windows;
using System.Windows.Input;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Loader
{
    public partial class LoaderGUI : Window
    {
        public LoaderGUI()
        {
            InitializeComponent();
            UpdateLabels();
            LoaderState.MarkStarted();
        }

        private void ToggleEsp_Click(object sender, RoutedEventArgs e)
        {
            FeatureToggle.EspEnabled = !FeatureToggle.EspEnabled;
            UpdateLabels();
        }

        private void ToggleAimAssist_Click(object sender, RoutedEventArgs e)
        {
            FeatureToggle.AimAssistEnabled = !FeatureToggle.AimAssistEnabled;
            UpdateLabels();
        }

        private void ToggleSnap_Click(object sender, RoutedEventArgs e)
        {
            FeatureToggle.SnapEnabled = !FeatureToggle.SnapEnabled;
            UpdateLabels();
        }

        private void ToggleOverlay_Click(object sender, RoutedEventArgs e)
        {
            FeatureToggle.VisualsOverlayEnabled = !FeatureToggle.VisualsOverlayEnabled;
            UpdateLabels();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UpdateLabels()
        {
            EspStatusLabel.Content = $"ESP: {(FeatureToggle.EspEnabled ? "ON" : "OFF")}";
            AimStatusLabel.Content = $"AIM: {(FeatureToggle.AimAssistEnabled ? "ON" : "OFF")}";
            SnapStatusLabel.Content = $"SNAP: {(FeatureToggle.SnapEnabled ? "ON" : "OFF")}";
            OverlayStatusLabel.Content = $"VISUALS: {(FeatureToggle.VisualsOverlayEnabled ? "ON" : "OFF")}";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            InputMap.HandleKeyDown(e.Key);
            UpdateLabels();
        }
    }
}
