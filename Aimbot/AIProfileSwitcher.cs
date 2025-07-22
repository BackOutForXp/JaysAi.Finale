//heavenly v3.0
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using System.Collections.Generic;

namespace JaysAi.Finale.Aimbot
{
    public static class AIProfileSwitcher
    {
        private static readonly Dictionary<string, string> _profileMap = new()
        {
            { "Sniper", "LongRangeAI" },
            { "SMG", "CloseCombatAI" },
            { "AR", "BalancedAI" }
        };

        private static string _activeProfile = "BalancedAI";

        public static void UpdateProfile(string weaponType)
        {
            if (_profileMap.ContainsKey(weaponType))
            {
                _activeProfile = _profileMap[weaponType];
                Logger.LogInfo($"[AIProfileSwitcher] Switched to profile: {_activeProfile}");
                LoadProfile(_activeProfile);
            }
        }

        private static void LoadProfile(string profileName)
        {
            // This would tie into ModelLoader or behavior pipelines later
            FeatureToggleManager.EnableOnlyForProfile(profileName);
        }

        public static string GetActiveProfile() => _activeProfile;
    }
}
