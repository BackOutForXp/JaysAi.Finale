using JaysAi.Finale.AI;
using JaysAi.Finale.Features;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Targeting;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale
{
    public class MainLoop
    {
        private readonly NeuralContext _neural;
        private readonly ESP _esp;
        private readonly AimAssist _aim;
        private bool _running;

        public MainLoop()
        {
            _neural = new NeuralContext();
            _esp = new ESP();
            _aim = new AimAssist(_neural.ProfileManager);
        }

        public void Start()
        {
            _running = true;

            while (_running)
            {
                List<Enemy> enemies = _esp.Scan();

                // Update AI memory
                _neural.MemoryManager.Update(enemies);

                // Train the AI
                _neural.Train();

                // Use profile-aware aim logic
                if (LearningToggleModule.Enabled)
                    _aim.Execute(enemies);

                System.Threading.Thread.Sleep(16); // ~60fps
            }

            _neural.Save();
        }

        public void Stop()
        {
            _running = false;
        }
    }
}
