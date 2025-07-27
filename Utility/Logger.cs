using System;

namespace JaysAi.Finale.Utility
{
    public static class Logger
    {
        public static bool EnableDebug { get; set; } = true;

        public static void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[INFO] {message}");
            Console.ResetColor();
        }

        public static void LogDebug(string message)
        {
            if (!EnableDebug) return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[DEBUG] {message}");
            Console.ResetColor();
        }

        public static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN] {message}");
            Console.ResetColor();
        }

        public static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
        }

        public static void LogFatal(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[FATAL] {message}");
            Console.ResetColor();
            Environment.Exit(1);
        }
    }
}
