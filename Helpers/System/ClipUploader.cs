// neural v3.0
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JaysAi.Finale.Helpers.System
{
    public static class ClipUploader
    {
        private static readonly HttpClient _httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public static async Task<string?> UploadAsync(string filePath, string uploadEndpoint, string? token = null)
        {
            if (!File.Exists(filePath))
                return null;

            try
            {
                using var form = new MultipartFormDataContent();
                using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));

                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("video/mp4");
                form.Add(fileContent, "file", Path.GetFileName(filePath));

                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using var response = await _httpClient.PostAsync(uploadEndpoint, form);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ClipUploader] Upload failed: {ex.Message}");
                return null;
            }
        }
    }
}
