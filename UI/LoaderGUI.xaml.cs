// neural v3.0
using System.Windows;
using JaysAi.Finale.Core;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Loader;

namespace JaysAi.Finale.UI.Loader
{
    public partial class LoaderGUI : Window
    {
        public LoaderGUI()
        {
            InitializeComponent();
            lblStatus.Text = "Loader initialized. Ready.";
        }

        private async void BtnLaunch_Click(object sender, RoutedEventArgs e)
        {
            lblStatus.Text = "Starting JaysAi Neural System...";

            // Initialize settings and core logic
            AppSettings.Load();
            SettingsManager.Instance.ApplyProfile("Default");

            // Run startup logic
            bool initialized = await LoaderStartup.InitializeAsync();
            if (!initialized)
            {
                lblStatus.Text = "Initialization failed.";
                return;
            }

            lblStatus.Text = "JaysAi loaded successfully.";
            new MainOverlayWindow().Show();
            this.Close();
        }
    }
}
