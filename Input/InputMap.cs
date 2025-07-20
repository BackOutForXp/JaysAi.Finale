//monarch v2.1 – Keyboard hotkey controller for toggling features
using System.Windows.Input;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Input
{
    public static class InputMap
    {
        public static void HandleKeyDown(Key key)
        {
            switch (key)
            {
                case Key.F1:
                    FeatureToggle.EspEnabled = !FeatureToggle.EspEnabled;
                    break;

                case Key.F2:
                    FeatureToggle.AimAssistEnabled = !FeatureToggle.AimAssistEnabled;
                    break;

                case Key.F3:
                    FeatureToggle.SnapEnabled = !FeatureToggle.SnapEnabled;
                    break;

                case Key.F4:
                    FeatureToggle.VisualsOverlayEnabled = !FeatureToggle.VisualsOverlayEnabled;
                    break;

                case Key.F5:
                    FeatureToggle.RecoilCompensationEnabled = !FeatureToggle.RecoilCompensationEnabled;
                    break;

                case Key.F6:
                    FeatureToggle.TriggerBotEnabled = !FeatureToggle.TriggerBotEnabled;
                    break;

                case Key.F12:
                    FeatureToggle.DisableAll();
                    break;
            }
        }
    }
}
