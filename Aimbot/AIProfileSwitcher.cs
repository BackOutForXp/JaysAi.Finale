// neural v3.0
using System;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Aimbot
{
    public class AIProfileSwitcher
    {
        private readonly WeaponProfile defaultProfile;
        private WeaponProfile currentProfile;
        private readonly ProfileManager profileManager;
        private string currentWeapon = string.Empty;

        public AIProfileSwitcher(ProfileManager profileManager)
        {
            this.profileManager = profileManager;
            this.defaultProfile = profileManager.GetDefaultProfile();
            this.currentProfile = defaultProfile;
        }

        public WeaponProfile GetActiveProfile() => currentProfile;

        public void UpdateProfile(string weaponName)
        {
            if (string.IsNullOrWhiteSpace(weaponName) || weaponName == currentWeapon)
                return;

            currentWeapon = weaponName;

            var profile = profileManager.GetProfileForWeapon(weaponName);
            if (profile != null)
            {
                currentProfile = profile;
                LogManager.Log($"[ProfileSwitcher] Switched to profile: {weaponName}");
            }
            else
            {
                currentProfile = defaultProfile;
                LogManager.Log($"[ProfileSwitcher] No match found, using default profile.");
            }
        }

        public void ResetToDefault()
        {
            currentProfile = defaultProfile;
            LogManager.Log("[ProfileSwitcher] Reset to default profile.");
        }
    }
}
