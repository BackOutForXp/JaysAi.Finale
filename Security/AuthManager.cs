// File: Security/AuthManager.cs
using System;
using System.Net.Http;
using System.Threading.Tasks;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Security
{
    public static class AuthManager
    {
        private static readonly HttpClient _httpClient = new();

        public static bool IsAuthenticated { get; private set; } = false;
        public static string? CurrentUsername { get; private set; }

        /// <summary>
        /// Initiates authentication via API or token check.
        /// </summary>
        public static async Task<bool> AuthenticateAsync(string username, string token)
        {
            Logger.Log("Authenticating user...");

            try
            {
                // Placeholder logic — replace with real auth check
                var response = await _httpClient.GetAsync($"https://your-auth-endpoint.com/validate?user={username}&token={token}");

                if (response.IsSuccessStatusCode)
                {
                    IsAuthenticated = true;
                    CurrentUsername = username;
                    Logger.Log($"Authentication successful for {username}");
                    return true;
                }

                Logger.Log("Authentication failed.");
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log($"Auth error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Logs out the current user and resets state.
        /// </summary>
        public static void Logout()
        {
            IsAuthenticated = false;
            CurrentUsername = null;
            Logger.Log("User logged out.");
        }

        /// <summary>
        /// Bypasses online check (for debug or offline testing).
        /// </summary>
        public static void DebugBypass(string fakeUser = "DEV_MODE")
        {
            IsAuthenticated = true;
            CurrentUsername = fakeUser;
            Logger.Log($"Bypass mode active as {fakeUser}");
        }
    }
}
