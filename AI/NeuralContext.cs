using JaysAi.Finale.AI;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale
{
    public class NeuralContext
    {
        public MemoryManager MemoryManager { get; }
        public TargetProfileManager ProfileManager { get; }
        public AITrainer Trainer { get; }
        public LearningSettings LearningSettings { get; private set; }

        public NeuralContext()
        {
            MemoryManager = new MemoryManager();
            ProfileManager = new TargetProfileManager();
            Trainer = new AITrainer(MemoryManager);
            LearningSettings = LearningSettings.Load();
            LearningSettings.SyncTo(ProfileManager);
        }

        public void Save()
        {
            LearningSettings.SyncFrom(ProfileManager);
            LearningSettings.Save();
        }

        public void Train()
        {
            if (LearningToggleModule.Enabled)
                Trainer.Train();
        }
    }
}
