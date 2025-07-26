// neural v3.0
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace JaysAi.Finale.Helpers.System
{
    public static class CrashLogger
    {
        private static readonly string CrashLogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CrashLogs");

        public static void Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                LogException("UnhandledException", ex);
        }

        private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            LogException("UnobservedTaskException", e.Exception);
            e.SetObserved();
        }

        public static void LogException(string context, Exception ex)
        {
            try
            {
                Directory.CreateDirectory(CrashLogDirectory);
                string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss");
                string logPath = Path.Combine(CrashLogDirectory, $"crash_{context}_{timestamp}.log");

                var sb = new StringBuilder();
                sb.AppendLine($"Timestamp: {DateTime.UtcNow:O}");
                sb.AppendLine($"Context: {context}");
                sb.AppendLine($"Exception: {ex.GetType().FullName}");
                sb.AppendLine($"Message: {ex.Message}");
                sb.AppendLine($"StackTrace:\n{ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    sb.AppendLine("---- Inner Exception ----");
                    sb.AppendLine($"Type: {ex.InnerException.GetType().FullName}");
                    sb.AppendLine($"Message: {ex.InnerException.Message}");
                    sb.AppendLine($"StackTrace:\n{ex.InnerException.StackTrace}");
                }

                File.WriteAllText(logPath, sb.ToString());
            }
            catch (Exception failEx)
            {
                Debug.WriteLine($"[CrashLogger] Failed to write crash log: {failEx.Message}");
            }
        }
    }
}
