// Neural v3.0 — EspCheckboxBinder.cs
using JaysAi.Finale.Features;
using System.Windows.Controls;

namespace JaysAi.Finale.Overlay
{
    public static class EspCheckboxBinder
    {
        public static void BindToCheckbox(System.Windows.Controls.CheckBox checkbox, ESPModuleManager espModule)
        {
            if (checkbox == null || espModule == null)
                return;

            // Set initial state based on ESP state
            checkbox.IsChecked = espModule.IsEnabled;

            // Update ESPModule when checkbox is clicked
            checkbox.Checked += (_, _) => espModule.Enable();
            checkbox.Unchecked += (_, _) => espModule.Disable();

            // Optional: react to programmatic changes
            espModule.OnStateChanged += state =>
            {
                checkbox.Dispatcher.Invoke(() => checkbox.IsChecked = state);
            };
        }
    }
}
