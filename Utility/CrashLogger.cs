using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace JaysAi.Finale.Utility
{
    public static class CrashLogger
    {
        private static readonly string LogDir = "Logs";

        public static void Hook()
        {
            if (!Directory.Exists(LogDir))
                Directory.CreateDirectory(LogDir);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                Write("AppDomainException", e.ExceptionObject as Exception);

            Application.Current.DispatcherUnhandledException += (s, e) =>
            {
                Write("DispatcherException", e.Exception);
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Write("TaskException", e.Exception);
                e.SetObserved();
            };
        }

        public static void Write(string title, Exception? ex)
        {
            try
            {
                var fileName = $"{title}_{DateTime.Now:yyyyMMdd_HHmmss}.log";
                var path = Path.Combine(LogDir, fileName);

                File.WriteAllText(path, ex?.ToString() ?? "Unknown error");

                Console.WriteLine($"[CrashLogger] Wrote crash log: {path}");
            }
            catch { /* failsafe */ }
        }
    }
}
// ======================= MONARCH INTEGRATION =======================
// ✅ Hooked in App.xaml.cs → logs all app-wide and UI thread crashes
// ✅ Creates Logs/ directory if missing
// ✅ Silently saves logs for review (helps debug rare bugs or field reports)
// - [ ] Later: Upload crash logs to SaaS endpoint (for elite tier diagnostics)
// ===================================================================

