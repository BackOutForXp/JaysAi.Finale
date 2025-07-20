using DiscordRPC;
using DiscordRPC.Logging;
using System;

namespace JaysAi.Finale.Utility
{
    public static class DiscordPresence
    {
        private static DiscordRpcClient? _client;

        public static void Initialize()
        {
            try
            {
                _client = new DiscordRpcClient("1219932018424684614"); // Replace with your Discord App ID
                _client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
                _client.Initialize();

                _client.SetPresence(new RichPresence()
                {
                    Details = "Monarch Mode Activated",
                    State = "Loader Armed",
                    Timestamps = Timestamps.Now,
                    Assets = new Assets()
                    {
                        LargeImageKey = "monarch_logo", // Must be uploaded in Discord Developer portal
                        LargeImageText = "JaysAi.Finale Loader"
                    }
                });

                Console.WriteLine("[DiscordPresence] RPC started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DiscordPresence] Error: {ex.Message}");
            }
        }

        public static void Shutdown()
        {
            _client?.Dispose();
            Console.WriteLine("[DiscordPresence] RPC shut down.");
        }
    }
}

// ======================= MONARCH INTEGRATION =======================
// ✅ Auto-launches when loader starts
// ✅ Shows Monarch status in Discord
// ✅ Supports icons + Twitch linking later
// - [ ] Add streamer sync + session share status
// - [ ] Add versioning + tier display in RPC (e.g. Owner Mode Active)
// ===================================================================
