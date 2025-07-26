// Neural v3.0 — MainWindow.xaml.cs
using System;
using System.Threading.Tasks;
using System.Windows;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            EspToggle.Checked += (_, _) => OverlayState.EspEnabled = true;
            EspToggle.Unchecked += (_, _) => OverlayState.EspEnabled = false;

            CrosshairToggle.Checked += (_, _) => OverlayState.CrosshairEnabled = true;
            CrosshairToggle.Unchecked += (_, _) => OverlayState.CrosshairEnabled = false;

            FovToggle.Checked += (_, _) => OverlayState.FovRingEnabled = true;
            FovToggle.Unchecked += (_, _) => OverlayState.FovRingEnabled = false;

            DebugConsoleToggle.Checked += (_, _) => OverlayState.DebugConsoleVisible = true;
            DebugConsoleToggle.Unchecked += (_, _) => OverlayState.DebugConsoleVisible = false;

            StartButton.Click += async (_, _) => await LaunchOverlay();
            ShutdownButton.Click += (_, _) => ShutdownSystem();
        }

        private async Task LaunchOverlay()
        {
            StatusText.Text = "Status: Launching...";
            try
            {
                await MainLauncher.StartAsync();
                StatusText.Text = "Status: Overlay Active";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Launch failed: {ex.Message}";
            }
        }

        private void ShutdownSystem()
        {
            MainLauncher.Shutdown();
            StatusText.Text = "Status: Shut down";
        }
    }
}
