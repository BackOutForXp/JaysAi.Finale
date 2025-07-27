// Neural v3.1 — WeaponStat.cs
using System;

namespace JaysAi.Finale.Data
{
    public class WeaponStat
    {
        public string Name { get; set; } = "Unknown";
        public float RecoilX { get; set; } = 0f;
        public float RecoilY { get; set; } = 0f;
        public float BulletSpeed { get; set; } = 1000f;
        public float BulletDrop { get; set; } = 0f;
        public float FireRate { get; set; } = 600f; // Rounds per minute

        public bool IsValid => !string.IsNullOrWhiteSpace(Name) && FireRate > 0;

        public float TimeBetweenShots => 60f / FireRate;

        public override string ToString()
        {
            return $"{Name} | Recoil: ({RecoilX}, {RecoilY}) | Speed: {BulletSpeed} | Drop: {BulletDrop} | RPM: {FireRate}";
        }
    }
}
