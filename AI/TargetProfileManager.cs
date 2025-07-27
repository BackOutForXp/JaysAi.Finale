using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class TargetProfileManager
    {
        private readonly Dictionary<int, TargetProfile> _profiles = new();

        public TargetProfile GetOrCreate(int enemyId)
        {
            if (!_profiles.TryGetValue(enemyId, out var profile))
            {
                profile = new TargetProfile(enemyId);
                _profiles[enemyId] = profile;
            }

            return profile;
        }

        public void UpdateConfidence(int enemyId, float snapSuccessRate)
        {
            var profile = GetOrCreate(enemyId);
            profile.UpdateConfidence(snapSuccessRate);
        }

        public IEnumerable<TargetProfile> GetAll()
        {
            return _profiles.Values;
        }

        public void Clear()
        {
            _profiles.Clear();
        }

        public bool TryGetProfile(int enemyId, out TargetProfile profile)
        {
            return _profiles.TryGetValue(enemyId, out profile);
        }
    }
}
