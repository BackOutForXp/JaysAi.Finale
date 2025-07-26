// neural v3.0
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;
using System.Collections.ObjectModel;

namespace JaysAi.Finale.UI.Settings
{
    public partial class ProfileManager : UserControl
    {
        private ObservableCollection<string> _profiles;

        public ProfileManager()
        {
            InitializeComponent();
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            _profiles = new ObservableCollection<string>(SettingsManager.Instance.GetAvailableProfiles());
            ProfileList.ItemsSource = _profiles;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileList.SelectedItem is string profile)
            {
                SettingsManager.Instance.ApplyProfile(profile);
                MessageBox.Show($"Loaded profile: {profile}", "Profile", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string profileName = Microsoft.VisualBasic.Interaction.InputBox("Enter profile name to save:", "Save Profile", "Default");
            if (!string.IsNullOrWhiteSpace(profileName))
            {
                SettingsManager.Instance.SaveCurrentProfile(profileName);
                LoadProfiles();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileList.SelectedItem is string profile)
            {
                if (MessageBox.Show($"Delete profile: {profile}?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SettingsManager.Instance.DeleteProfile(profile);
                    LoadProfiles();
                }
            }
        }
    }
}
