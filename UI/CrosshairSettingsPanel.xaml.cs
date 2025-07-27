using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Loader;

namespace JaysAi.Finale.UI
{
    public partial class CrosshairSettingsPanel : UserControl
    {
        private AppSettings _settings => LoaderBootstrap.Settings;

        public CrosshairSettingsPanel()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Initialize UI controls from settings
            CrosshairSizeSlider.Value = _settings.CrosshairSize;
            CrosshairThicknessSlider.Value = _settings.CrosshairThickness;
            CrosshairDotCheckbox.IsChecked = _settings.EnableCrosshairDot;
            CrosshairCircleCheckbox.IsChecked = _settings.EnableCrosshairCircle;

            CrosshairColorDropdown.SelectedIndex = GetColorIndex(_settings.CrosshairColor);

            // Bind events
            CrosshairSizeSlider.ValueChanged += (_, args) =>
                _settings.CrosshairSize = (float)args.NewVal_
