//monarch v2.1 – Dynamic AI-Based Weapon Profile Switching
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.AI;
using System;

namespace JaysAi.Finale.Aimbot
{
    public class AIProfileSwitcher
    {
        private readonly WeaponProfileManager _profileManager;
        private string _lastDetectedWeapon = string.Empty;

        public AIProfileSwitcher(WeaponProfileManager profileManager)
        {
            _profileManager = profileManager;
        }

        public void Update()
        {
            // Simulate weapon detection via YOLO or OCR bridge
            string detectedWeapon = ModelLoader.CurrentDetectedWeapon;

            if (!string.IsNullOrEmpty(detectedWeapon) && detectedWeapon != _lastDetectedWeapon)
            {
                Console.WriteLine($"[AI Switcher] New weapon detected: {detectedWeapon}");
                _profileManager.SetActiveWeapon(detectedWeapon);
                _lastDetectedWeapon = detectedWeapon;
            }
        }
    }
}
