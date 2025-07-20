// File: Utility/ClipUploader.cs
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace JaysAi.Finale.Utility
{
    public static class ClipUploader
    {
        private static readonly HttpClient _httpClient = new();

        /// <summary>
        /// Uploads a clip or screenshot to an image/file hosting service (e.g., Discord webhook, Imgur, etc.).
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Public URL or upload status</returns>
        public static async Task<string?> UploadClipAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Logger.Log($"Clip not found: {filePath}");
                return null;
            }

            try
            {
                Logger.Log($"Uploading clip: {filePath}");

                using var form = new MultipartFormDataContent();
                using var fileStream = File.OpenRead(filePath);

                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("video/mp4"); // or image/png

                form.Add(streamContent, "file", Path.GetFileName(filePath));

                // TODO: Replace with your upload target
                string uploadUrl = "https://your.upload.api/endpoint";
                var response = await _httpClient.PostAsync(uploadUrl, form);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                Logger.Log($"Upload successful: {result}");

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log($"Upload failed: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Launches local folder where clips are stored.
        /// </summary>
        public static void OpenClipFolder()
        {
            string clipPath = Path.Combine(Paths.BaseDirectory, "Clips");
            Directory.CreateDirectory(clipPath);
            Process.Start("explorer.exe", clipPath);
        }
    }
}
