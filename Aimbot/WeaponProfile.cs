//monarch v2.1 – Weapon-Specific Profile Logic
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Aimbot
{
    public class WeaponProfile
    {
        public string Name { get; set; }
        public List<Vector2> RecoilPattern { get; set; }
        public float RecoilScale { get; set; }
        public float SnapStrength { get; set; }
        public float MaxSnapDistance { get; set; }

        public WeaponProfile(string name)
        {
            Name = name;
            RecoilPattern = new List<Vector2>();
            RecoilScale = 1.0f;
            SnapStrength = 1.0f;
            MaxSnapDistance = 100.0f;
        }
    }

    public class WeaponProfileManager
    {
        private readonly Dictionary<string, WeaponProfile> _profiles = new();
        private WeaponProfile _currentProfile;

        public void AddProfile(WeaponProfile profile)
        {
            if (!_profiles.ContainsKey(profile.Name))
                _profiles[profile.Name] = profile;
        }

        public void SetActiveWeapon(string weaponName)
        {
            if (_profiles.TryGetValue(weaponName, out var profile))
            {
                _currentProfile = profile;
            }
        }

        public WeaponProfile GetCurrentProfile()
        {
            return _currentProfile;
        }

        public RecoilManager GetRecoilManager()
        {
            if (_currentProfile == null)
                return new RecoilManager(new List<Vector2>());

            return new RecoilManager(_currentProfile.RecoilPattern, _currentProfile.RecoilScale);
        }

        public float GetSnapStrength()
        {
            return _currentProfile?.SnapStrength ?? 1.0f;
        }

        public float GetMaxSnapDistance()
        {
            return _currentProfile?.MaxSnapDistance ?? 100.0f;
        }
    }
}
