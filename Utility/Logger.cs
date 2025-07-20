//monarch v1.0
using System;
using System.IO;

namespace JaysAi.Utility
{
    public static class Logger
    {
        private static readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");

        static Logger()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                File.AppendAllText(logFilePath, $"--- Logging started: {DateTime.Now} ---{Environment.NewLine}");
            }
            catch
            {
                // Safe-fail: Logging is optional
            }
        }

        public static void Log(string message)
        {
            try
            {
                string logEntry = $"{DateTime.Now:HH:mm:ss} | {message}";
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                Console.WriteLine(logEntry); // Optional: Live output for debug console
            }
            catch
            {
                // Safe-fail: Log error shouldn't break anything
            }
        }

        public static void LogException(Exception ex)
        {
            Log($"[ERROR] {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
        }
    }
}
