// Neural v3.1
using System.Windows.Controls;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.UI
{
    public static class AiOverlayCheckboxBinder
    {
        public static void Bind(System.Windows.Controls.CheckBox checkbox)
        {
            const string key = "AiOverlayEnabled";

            checkbox.IsChecked = UserSettings.Instance.Get(key, true);

            checkbox.Checked += (_, _) => UserSettings.Instance.Set(key, true);
            checkbox.Unchecked += (_, _) => UserSettings.Instance.Set(key, false);
        }
    }
}
