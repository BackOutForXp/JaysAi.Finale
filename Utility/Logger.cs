//monarch v2.1 – Unified Logging Framework
using System;
using System.Diagnostics;
using System.IO;

namespace JaysAi.Finale.Utility
{
    public static class Logger
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string LogFile = Path.Combine(LogDirectory, $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        private static bool _consoleEnabled = true;

        static Logger()
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);
            }
            catch { /* Silently fail to avoid crashing */ }
        }

        public static void EnableConsoleOutput(bool enable) => _consoleEnabled = enable;

        public static void Info(string message) => Write("INFO", message);
        public static void Warn(string message) => Write("WARN", message);
        public static void Error(string message) => Write("ERROR", message);
        public static void Debug(string message)
        {
#if DEBUG
            Write("DEBUG", message);
#endif
        }

        private static void Write(string level, string message)
        {
            string output = $"[{DateTime.Now:HH:mm:ss}] [{level}] {message}";
            try
            {
                File.AppendAllText(LogFile, output + Environment.NewLine);
            }
            catch { /* Ignore file write failures */ }

            if (_consoleEnabled)
            {
                ConsoleColor previous = Console.ForegroundColor;
                Console.ForegroundColor = level switch
                {
                    "ERROR" => ConsoleColor.Red,
                    "WARN" => ConsoleColor.Yellow,
                    "DEBUG" => ConsoleColor.Cyan,
                    _ => ConsoleColor.White
                };
                Console.WriteLine(output);
                Console.ForegroundColor = previous;
            }

            Trace.WriteLine(output);
        }
    }
}
