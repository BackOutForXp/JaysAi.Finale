// Neural v3.1 — OverlayToggleHotkey.cs
using System.Windows.Input;

namespace JaysAi.Finale.Overlay
{
    public class OverlayToggleHotkey
    {
        public string FeatureName { get; set; }
        public Key ToggleKey { get; set; }
        public bool IsToggled { get; set; }

        public OverlayToggleHotkey(string featureName, Key toggleKey)
        {
            FeatureName = featureName;
            ToggleKey = toggleKey;
            IsToggled = false;
        }

        public void Update(Key currentKey)
        {
            if (currentKey == ToggleKey)
            {
                IsToggled = !IsToggled;
                Features.FeatureToggleManager.Set(FeatureName, IsToggled);
            }
        }
    }
}
