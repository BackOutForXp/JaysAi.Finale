// neural v3.0
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Input;
using JaysAi.Finale.Input.Keybinds;

namespace JaysAi.Finale.Overlay.Panels
{
    public partial class KeybindSettingsPanel : UserControl
    {
        public KeybindSettingsPanel()
        {
            InitializeComponent();
            Loaded += KeybindSettingsPanel_Loaded;
        }

        private void KeybindSettingsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            AimKeyBind.Text = UserSettings.Instance.Keybinds.AimKey.ToString();
            ESPKeyBind.Text = UserSettings.Instance.Keybinds.ESPKey.ToString();
        }

        private void AimKeyBind_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Enum.TryParse(AimKeyBind.Text, out System.Windows.Input.Key newKey))
            {
                UserSettings.Instance.Keybinds.AimKey = newKey;
            }
        }

        private void ESPKeyBind_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Enum.TryParse(ESPKeyBind.Text, out System.Windows.Input.Key newKey))
            {
                UserSettings.Instance.Keybinds.ESPKey = newKey;
            }
        }
    }
}
