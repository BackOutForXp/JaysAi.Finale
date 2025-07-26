// neural v3.0
using System;
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Input;

namespace JaysAi.Finale.Aimbot
{
    public class AutoTrigger
    {
        private readonly IEnemyProvider enemyProvider;
        private readonly InputInjector inputInjector;
        private readonly TriggerSettings triggerSettings;

        public AutoTrigger(IEnemyProvider enemyProvider, InputInjector inputInjector, TriggerSettings triggerSettings)
        {
            this.enemyProvider = enemyProvider;
            this.inputInjector = inputInjector;
            this.triggerSettings = triggerSettings;
        }

        public void Execute()
        {
            if (!triggerSettings.Enabled || !SystemStatus.IsInGame)
                return;

            var target = enemyProvider.GetTargetUnderCrosshair();
            if (target != null && target.IsVisible && target.IsAlive)
            {
                inputInjector.PressFire(triggerSettings.FireHoldTimeMs);
                LogManager.Log("[AutoTrigger] Fired at target under crosshair.");
            }
        }
    }
}
