// neural v3.0
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.SystemLogic
{
    public static class ExceptionHandler
    {
        private static readonly string LogDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "JaysAi", "Logs");

        public static void AttachGlobalHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                LogException(ex, "UnhandledException");
            }
        }

        public static void LogException(Exception ex, string context = "General")
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                string logFile = Path.Combine(LogDirectory,
                    $"Exception_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");

                string content = $"[{DateTime.Now}] [{context}]\n" +
                                 $"{ex.GetType().Name}: {ex.Message}\n" +
                                 $"Stack Trace:\n{ex.StackTrace}\n";

                File.AppendAllText(logFile, content);

                // Also optionally log to internal logger if available
                LogManager.Log($"[EXCEPTION][{context}] {ex.Message}\n{ex.StackTrace}");
            }
            catch
            {
                // fallback if even logging fails
                Debug.WriteLine("Failed to log exception.");
            }
        }

        public static void Handle(Action action, string context = "SafeBlock")
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogException(ex, context);
            }
        }
    }
}
