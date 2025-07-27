using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Loader;

namespace JaysAi.Finale.UI
{
    public partial class ProfileManagerPanel : UserControl
    {
        private SettingsManager _manager => LoaderBootstrap.SettingsManager;
        private AppSettings _settings => LoaderBootstrap.Settings;

        public ProfileManagerPanel()
        {
            InitializeComponent();
            LoadProfileList();

            LoadButton.Click += (_, _) => LoadSelectedProfile();
            SaveButton.Click += (_, _) => SaveCurrentProfile();
            DeleteButton.Click += (_, _) => DeleteSelectedProfile();
            SaveAsNewButton.Click += (_, _) => SaveAsNewProfile();
        }

        private void LoadProfileList()
        {
            ProfileDropdown.ItemsSource = _manager.GetAvailableProfiles();
            if (ProfileDropdown.Items.Count > 0)
                ProfileDropdown.SelectedIndex = 0;
        }

        private void LoadSelectedProfile()
        {
            if (ProfileDropdown.SelectedItem is string profile)
            {
                var loaded = _manager.Load<AppSettings>(profile);
                if (loaded != null)
                {
                    LoaderBootstrap.Settings = loaded;
                    MessageBox.Show($"Loaded profile: {profile}", "JaysAi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void SaveCurrentProfile()
        {
            if (ProfileDropdown.SelectedItem is string profile)
            {
                _manager.Save(profile, _settings);
                MessageBox.Show($"Saved profile: {profile}", "JaysAi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveAsNewProfile()
        {
            string name = NewProfileNameBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                _manager.Save(name, _settings);
                LoadProfileList();
                MessageBox.Show($"Profile '{name}' saved.", "JaysAi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteSelectedProfile()
        {
            if (ProfileDropdown.SelectedItem is string profile)
            {
                _manager.Delete(profile);
                LoadProfileList();
                MessageBox.Show($"Deleted profile: {profile}", "JaysAi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
