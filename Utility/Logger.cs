//monarch v2.1 – Runtime Debug Logger
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Utility
{
    public static class Logger
    {
        private static readonly List<string> LogHistory = new();

        public static void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string formatted = $"[{timestamp}] {message}";
            Console.WriteLine(formatted);
            LogHistory.Add(formatted);

            // Optional: Forward to GUI, file, or overlay here
            // e.g., OverlaySignal.Push(formatted);
        }

        public static void Clear()
        {
            LogHistory.Clear();
            Console.Clear();
        }

        public static IEnumerable<string> GetLogHistory()
        {
            return LogHistory.ToArray();
        }

        public static void Warn(string message) => Log($"[WARN] {message}");
        public static void Error(string message) => Log($"[ERROR] {message}");
        public static void Info(string message) => Log($"[INFO] {message}");
        public static void Success(string message) => Log($"[SUCCESS] {message}");
    }
}
