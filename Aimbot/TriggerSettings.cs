//monarch v2.1
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.Aimbot
{
    public class AutoTrigger
    {
        private readonly TriggerSettings settings;
        private float lastShotTime;
        private int burstShotsRemaining;

        public AutoTrigger(TriggerSettings settings)
        {
            this.settings = settings;
            ResetBurst();
        }

        public void TryFire(FrameSnapshot? currentTarget, float currentTime)
        {
            if (currentTarget == null || !currentTarget.IsVisible)
                return;

            if (settings.BurstEnabled)
            {
                if (burstShotsRemaining > 0 && currentTime - lastShotTime >= settings.BurstDelay)
                {
                    InputInjector.Fire();
                    lastShotTime = currentTime;
                    burstShotsRemaining--;
                }
            }
            else if (currentTime - lastShotTime >= settings.FireDelay)
            {
                InputInjector.Fire();
                lastShotTime = currentTime;
            }
        }

        public void ResetBurst()
        {
            burstShotsRemaining = settings.BurstSize;
        }
    }
}

