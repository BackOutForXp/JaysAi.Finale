// heavenly v3.0 – Unified AI Orchestration Logic
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Core
{
    public static class AiOrchestrator
    {
        private static bool _isRunning;

        public static void Start()
        {
            if (_isRunning) return;

            AiManager.Initialize();
            ModuleManager.Initialize();
            BehaviorTrigger.Initialize();
            RuntimeBehaviorLog.Initialize();

            _isRunning = true;
        }

        public static void Execute()
        {
            if (!_isRunning)
                Start();

            GameMemory.Refresh(); // Sync latest game memory data
            AiManager.Update();   // Main detection and targeting
            ModuleManager.Tick(); // Run all active modules
            InputDispatcher.Tick(); // Inject inputs based on decisions
        }

        public static void Stop()
        {
            _isRunning = false;
        }
    }
}
