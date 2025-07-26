// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Data;
using JaysAi.Finale.Input;
using JaysAi.Finale.SystemLogic;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Core
{
    public class AiOrchestrator
    {
        private readonly TargetingSystem targetingSystem;
        private readonly PredictionEngine predictionEngine;
        private readonly BehaviorTrigger behaviorTrigger;
        private readonly ModuleManager moduleManager;

        public AiOrchestrator(
            TargetingSystem targetingSystem,
            PredictionEngine predictionEngine,
            BehaviorTrigger behaviorTrigger,
            ModuleManager moduleManager)
        {
            this.targetingSystem = targetingSystem;
            this.predictionEngine = predictionEngine;
            this.behaviorTrigger = behaviorTrigger;
            this.moduleManager = moduleManager;
        }

        public void RunCycle(FrameSnapshot frame, InputSnapshot input)
        {
            if (!frame.IsValid) return;

            List<TargetInfo> targets = targetingSystem.EvaluateTargets(frame);
            TargetInfo selectedTarget = targetingSystem.SelectOptimalTarget(targets, input);

            if (selectedTarget == null)
            {
                moduleManager.DisableAll();
                return;
            }

            predictionEngine.ApplyPrediction(ref selectedTarget, frame, input);

            if (behaviorTrigger.ShouldTrigger(selectedTarget, input))
            {
                moduleManager.Execute(selectedTarget, input);
            }
            else
            {
                moduleManager.DisableAll();
            }
        }

        public void ResetState()
        {
            moduleManager?.ResetModules();
        }
    }
}
