//monarch v2.0
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public static class ProgressMemoryReader
    {
        public static void InjectProgressData(Dictionary<int, (int health, int armor)> progressMap)
        {
            foreach (var kvp in progressMap)
            {
                if (!AiMemory.Entities.ContainsKey(kvp.Key))
                    continue;

                var entity = AiMemory.Entities[kvp.Key];
                string label = BuildLabel(entity, kvp.Value.health, kvp.Value.armor);
                AiMemory.UpdateEntity(entity.Id, entity.ScreenPosition, entity.IsEnemy, label);
            }
        }

        private static string BuildLabel(EntityData entity, int health, int armor)
        {
            if (!entity.IsEnemy) return "";

            string healthText = health switch
            {
                >= 100 => "💯",
                >= 75 => "🔋",
                >= 50 => "⚠️",
                >= 25 => "🩸",
                _ => "☠️"
            };

            string armorText = armor switch
            {
                >= 100 => "🛡️",
                >= 50 => "🔰",
                > 0 => "🪖",
                _ => ""
            };

            return $"{healthText} {armorText}".Trim();
        }
    }
}
