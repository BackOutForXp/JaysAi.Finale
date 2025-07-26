// neural v3.0
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JaysAi.Finale.Models;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Security
{
    public sealed class AuthManager
    {
        private static readonly Lazy<AuthManager> _instance = new(() => new AuthManager());
        public static AuthManager Instance => _instance.Value;

        private string? _authToken;
        private UserProfile? _currentUser;
        private readonly HttpClient _httpClient;

        private AuthManager()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var payload = new
                {
                    user = username,
                    pass = password
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://api.jaysai.io/auth", content);

                if (!response.IsSuccessStatusCode)
                    return false;

                var result = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<AuthResponse>(result);

                if (data == null || string.IsNullOrWhiteSpace(data.Token))
                    return false;

                _authToken = data.Token;
                _currentUser = data.Profile;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"[AuthManager] Login failed: {ex.Message}", LogLevel.Error);
                return false;
            }
        }

        public string? GetToken() => _authToken;

        public UserProfile? GetUser() => _currentUser;

        public void Logout()
        {
            _authToken = null;
            _currentUser = null;
        }

        private class AuthResponse
        {
            public string Token { get; set; } = string.Empty;
            public UserProfile Profile { get; set; } = new();
        }
    }
}
