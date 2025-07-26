// neural v3.0
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JaysAi.Finale.Utility
{
    public static class HttpService
    {
        private static readonly HttpClient _client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        static HttpService()
        {
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("JaysAi.Finale/Neural-Client");
        }

        public static async Task<string?> GetAsync(string url)
        {
            try
            {
                using var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                LogError("GET", url, ex);
                return null;
            }
        }

        public static async Task<string?> PostAsync(string url, string jsonBody)
        {
            try
            {
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                using var response = await _client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                LogError("POST", url, ex);
                return null;
            }
        }

        public static async Task<string?> PutAsync(string url, string jsonBody)
        {
            try
            {
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                using var response = await _client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                LogError("PUT", url, ex);
                return null;
            }
        }

        public static async Task<bool> DeleteAsync(string url)
        {
            try
            {
                using var response = await _client.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                LogError("DELETE", url, ex);
                return false;
            }
        }

        private static void LogError(string method, string url, Exception ex)
        {
            Console.WriteLine($"[HttpService] {method} {url} failed: {ex.Message}");
        }
    }
}
