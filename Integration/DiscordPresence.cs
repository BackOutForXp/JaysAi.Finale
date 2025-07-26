// neural v3.0
using System;
using DiscordRPC;
using DiscordRPC.Logging;

namespace JaysAi.Finale.Integration
{
    public static class DiscordPresence
    {
        private static DiscordRpcClient? _client;

        public static void Initialize(string clientId = "1170000000000000000") // Use your real App ID
        {
            try
            {
                _client = new DiscordRpcClient(clientId)
                {
                    Logger = new ConsoleLogger() { Level = LogLevel.Warning }
                };

                _client.OnReady += (sender, args) =>
                    Console.WriteLine($"[Discord] Connected as {args.User.Username}");

                _client.OnError += (sender, args) =>
                    Console.WriteLine($"[Discord] Error: {args.Message}");

                _client.Initialize();

                SetDefaultPresence();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DiscordPresence] Init failed: {ex.Message}");
            }
        }

        public static void SetDefaultPresence()
        {
            if (_client == null || !_client.IsInitialized)
                return;

            _client.SetPresence(new RichPresence
            {
                Details = "AI Gaming Intelligence",
                State = "Monitoring Performance",
                Assets = new Assets
                {
                    LargeImageKey = "logo_large", // Must match Discord Developer Portal
                    LargeImageText = "JaysAi Neural Loader"
                },
                Timestamps = Timestamps.Now
            });
        }

        public static void
