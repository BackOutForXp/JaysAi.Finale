// neural v3.0
namespace JaysAi.Finale.Aimbot
{
    public class WeaponProfile
    {
        public string WeaponName { get; set; } = "Default";

        public float AimFov { get; set; } = 12.0f;
        public float AimSmoothness { get; set; } = 1.25f;
        public float RecoilCompensation { get; set; } = 0.8f;
        public float BulletDropCompensation { get; set; } = 1.0f;
        public float FireRateMultiplier { get; set; } = 1.0f;

        public bool UsePrediction { get; set; } = true;
        public bool UseSnapAssist { get; set; } = false;
        public bool EnableRecoilControl { get; set; } = true;

        public int BurstCount { get; set; } = 0;
        public int DelayBetweenBursts { get; set; } = 0;

        public WeaponProfile Clone()
        {
            return (WeaponProfile)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{WeaponName} [FOV: {AimFov}, Smooth: {AimSmoothness}, Recoil: {RecoilCompensation}]";
        }
    }
}
