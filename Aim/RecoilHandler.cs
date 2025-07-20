//monarch v2.1
using System.Collections.Generic;

namespace JaysAi.Finale.Aim
{
    public class RecoilHandler
    {
        private readonly Dictionary<string, RecoilPattern> weaponPatterns = new();
        private RecoilPattern? currentPattern;

        public void LoadPattern(string weaponName, RecoilPattern pattern)
        {
            weaponPatterns[weaponName] = pattern;
        }

        public void SelectWeapon(string weaponName)
        {
            weaponPatterns.TryGetValue(weaponName, out currentPattern);
        }

        public (float offsetX, float offsetY) GetOffset(int bulletIndex)
        {
            if (currentPattern == null || bulletIndex >= currentPattern.Steps.Count)
                return (0f, 0f);

            return currentPattern.Steps[bulletIndex];
        }

        public void Reset()
        {
            currentPattern = null;
        }
    }
}
