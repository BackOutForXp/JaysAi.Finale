// neural v3.0
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JaysAi.Finale.UI.Windows
{
    public partial class LicensePromptWindow : Window
    {
        public LicensePromptWindow()
        {
            InitializeComponent();

            ActivateButton.Click += OnActivateClick;
            CloseButton.Click += (_, _) => Close();
        }

        private async void OnActivateClick(object sender, RoutedEventArgs e)
        {
            string licenseKey = LicenseInput.Text.Trim();

            if (string.IsNullOrEmpty(licenseKey))
            {
                ShowStatus("Please enter a license key.");
                return;
            }

            ActivateButton.IsEnabled = false;
            ShowStatus("Validating license key...");

            bool isValid = await ValidateLicenseKeyAsync(licenseKey);

            if (isValid)
            {
                ShowStatus("License activated successfully!", success: true);
                await Task.Delay(1000);
                DialogResult = true;
                Close();
            }
            else
            {
                ShowStatus("Invalid or expired license key.");
                ActivateButton.IsEnabled = true;
            }
        }

        private async Task<bool> ValidateLicenseKeyAsync(string key)
        {
            // Replace this logic with your real API call
            await Task.Delay(500); // Simulated API delay

            // TEMP: Mock validation
            return key == "JAYSAI-UNLOCKED-2025"; // 🔒 Replace with your actual backend logic
        }

        private void ShowStatus(string message, bool success = false)
        {
            StatusText.Text = message;
            StatusText.Foreground = success ? System.Windows.Media.Brushes.LightGreen : System.Windows.Media.Brushes.IndianRed;
        }
    }
}
