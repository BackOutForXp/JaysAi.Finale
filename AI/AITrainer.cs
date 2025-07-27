using System.Collections.Generic;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.AI
{
    public class AITrainer
    {
        private readonly MemoryManager _memoryManager;

        public AITrainer(MemoryManager memoryManager)
        {
            _memoryManager = memoryManager;
        }

        public void Train()
        {
            foreach (TargetMemory memory in _memoryManager.GetAll())
            {
                if (!memory.IsHighConfidence()) continue;

                AdjustWeightingFor(memory);
            }
        }

        private void AdjustWeightingFor(TargetMemory memory)
        {
            // This is the core of adaptive logic
            // You can adjust aim weighting, bone preference, prediction delta, etc.

            float successRate = memory.GetSnapSuccessRate();

            if (successRate > 0.8f)
            {
                // Maybe trust headshots more
                AimPreferences.SetBonePreference(memory.EnemyId, BoneTarget.Head);
            }
            else if (successRate > 0.5f)
            {
                AimPreferences.SetBonePreference(memory.EnemyId, BoneTarget.Chest);
            }
            else
            {
                // Low accuracy — deprioritize or snap to center
                AimPreferences.SetBonePreference(memory.EnemyId, BoneTarget.Stomach);
            }
        }
    }
}
