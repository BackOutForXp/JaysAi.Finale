// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Security;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Input;
using JaysAi.Finale.Data;
using System;

namespace JaysAi.Finale.Core
{
    public class MainInjection
    {
        private ModuleManager moduleManager;
        private TargetingSystem targetingSystem;
        private PredictionEngine predictionEngine;
        private BehaviorTrigger behaviorTrigger;
        private AiOrchestrator orchestrator;
        private InputManager inputManager;
        private GameProcessHelper processHelper;

        public void Initialize()
        {
            try
            {
                LogManager.Info("MainInjection: Initialization started.");

                // Scan and validate target process
                processHelper = new GameProcessHelper();
                if (!processHelper.AttachToGame())
                {
                    LogManager.Error("MainInjection: Failed to attach to game process.");
                    return;
                }

                LogManager.Info("Game process attached.");

                // Build AI components
                moduleManager = new ModuleManager();
                targetingSystem = new TargetingSystem();
                predictionEngine = new PredictionEngine();
                behaviorTrigger = new BehaviorTrigger();
                inputManager = new InputManager();

                orchestrator = new AiOrchestrator(
                    targetingSystem,
                    predictionEngine,
                    behaviorTrigger,
                    moduleManager
                );

                LogManager.Info("All systems initialized successfully.");
            }
            catch (Exception ex)
            {
                LogManager.Exception("MainInjection.Initialize", ex);
            }
        }

        public void Tick()
        {
            if (!processHelper?.IsProcessActive() ?? true) return;

            var currentFrame = FrameSnapshot.Capture();
            var input = inputManager.CaptureSnapshot();

            orchestrator?.RunCycle(currentFrame, input);
        }

        public void Shutdown()
        {
            LogManager.Info("MainInjection: Shutting down...");
            orchestrator?.ResetState();
        }
    }
}
