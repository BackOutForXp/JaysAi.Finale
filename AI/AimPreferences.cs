using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public static class AimPreferences
    {
        private static readonly Dictionary<int, BoneTarget> _boneMap = new();

        public static void SetBonePreference(int enemyId, BoneTarget target)
        {
            _boneMap[enemyId] = target;
        }

        public static BoneTarget GetBonePreference(int enemyId)
        {
            return _boneMap.TryGetValue(enemyId, out var target)
                ? target
                : BoneTarget.Chest; // default fallback
        }

        public static void Clear()
        {
            _boneMap.Clear();
        }

        public static IReadOnlyDictionary<int, BoneTarget> AllPreferences => _boneMap;
    }
}
