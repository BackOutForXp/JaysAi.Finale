//heavenly v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Aim;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Modules;

namespace JaysAi.Finale.Aimbot
{
    public class WeaponProfile
    {
        public string Name { get; set; }
        public float AimSmoothing { get; set; }
        public float AimFov { get; set; }
        public bool UseRecoilCompensation { get; set; }
        public RecoilPattern RecoilPattern { get; set; }
        public float FireRate { get; set; } // Rounds per second
        public bool AutoFire { get; set; }
        public TriggerSettings TriggerConfig { get; set; }
        public float PredictionFactor { get; set; }

        public WeaponProfile(string name)
        {
            Name = name;
            AimSmoothing = 5.0f;
            AimFov = 12.0f;
            UseRecoilCompensation = true;
            RecoilPattern = new RecoilPattern();
            FireRate = 9.5f;
            AutoFire = false;
            TriggerConfig = new TriggerSettings();
            PredictionFactor = 1.0f;
        }

        public bool IsViableFor(TrackedTarget target)
        {
            return target != null && target.IsAlive && target.Distance < 100f;
        }
    }
}
