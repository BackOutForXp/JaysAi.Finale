//monarch v2.1 – Fully Refactored & Synced

using global::System;
using global::System.Threading.Tasks;
using JaysAi.Finale.AI;
using JaysAi.Finale.Aim;
using JaysAi.Finale.Aimbot;
using JaysAi.Finale.Input;
using JaysAi.Finale.Modules;
using JaysAi.Finale.SystemLogic;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.SystemLogic
{
    public static class MainLauncher
    {
        public static async Task InitializeAsync()
        {
            try
            {
                LogManager.Log("⏳ Initializing JaysAi.Monarch...");

                string buildInfo = BuildInfo.GetDetailedBuildInfo();
                LogManager.Log($"🔧 Build Info: {buildInfo}");

                UsersSettings.Load();
                LogManager.Log("⚙️ User settings loaded.");

                await ESPModule.InitializeAsync();
                await SnapAssist.InitializeAsync();
                await OverlaySignal.InitializeAsync();
                InputManager.Initialize();

                LogManager.Log("✅ All modules successfully initialized.");
            }
            catch (Exception ex)
            {
                LogManager.Log($"❌ Fatal error in MainLauncher: {ex.Message}", LogLevel.Error);
            }
        }
    }
}
