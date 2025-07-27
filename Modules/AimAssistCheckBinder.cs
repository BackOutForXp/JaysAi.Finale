// Neural v3.1
using JaysAi.Finale.Settings;
using System.Windows.Controls;

namespace JaysAi.Finale.UI
{
    public static class AimAssistCheckboxBinder
    {
        public static void Bind(System.Windows.Controls.CheckBox checkbox, string settingKey = "AimAssistEnabled")
        {
            checkbox.IsChecked = UserSettings.Instance.Get(settingKey, false);

            checkbox.Checked += (_, _) => UserSettings.Instance.Set(settingKey, true);
            checkbox.Unchecked += (_, _) => UserSettings.Instance.Set(settingKey, false);
        }
    }
}
