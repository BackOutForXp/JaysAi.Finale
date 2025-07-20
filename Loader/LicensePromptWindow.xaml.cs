using System.Windows;
using JaysAi.Finale.System;

namespace JaysAi.Finale.Loader
{
    public partial class LicensePromptWindow : Window
    {
        public string? ValidLicense { get; private set; }

        public LicensePromptWindow()
        {
            InitializeComponent();
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            string key = KeyBox.Text.Trim();

            if (LicenseManager.Validate(key))
            {
                ValidLicense = key;
                LicenseManager.Save(key);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Invalid license key.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
