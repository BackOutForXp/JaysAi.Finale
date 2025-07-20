// File: UI/ProfileManagerPanel.xaml.cs
using JaysAi.Finale.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JaysAi.Finale.UI
{
    public partial class ProfileManagerPanel : UserControl
    {
        private string ProfilesFolder => SettingsManager<AppSettings>.ProfilesFolder;
        private List<string> Profiles => Directory.Exists(ProfilesFolder)
            ? Directory.GetFiles(ProfilesFolder, "*.json").Select(Path.GetFileNameWithoutExtension).ToList()
            : new List<string>();

        public ProfileManagerPanel()
        {
            InitializeComponent();
            RefreshProfileList();
        }

        private void RefreshProfileList()
        {
            ProfileDropdown.ItemsSource = Profiles;
        }

        private void LoadProfile_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileDropdown.SelectedItem is string name)
            {
                SettingsManager<AppSettings>.LoadProfile(name);
                MessageBox.Show($"Loaded profile: {name}");
            }
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileDropdown.SelectedItem is string name)
            {
                SettingsManager<AppSettings>.SaveProfile(name);
                MessageBox.Show($"Saved profile: {name}");
            }
        }

        private void DeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileDropdown.SelectedItem is string name)
            {
                var path = Path.Combine(ProfilesFolder, $"{name}.json");
                if (File.Exists(path))
                {
                    File.Delete(path);
                    MessageBox.Show($"Deleted profile: {name}");
                    RefreshProfileList();
                }
            }
        }

        private void SaveAsNewProfile_Click(object sender, RoutedEventArgs e)
        {
            string name = NewProfileNameBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                SettingsManager<AppSettings>.SaveProfile(name);
                MessageBox.Show($"Saved new profile: {name}");
                RefreshProfileList();
            }
        }
    }
}
