using System;
using System.Collections.Generic;
using System.IO;

namespace JaysAi.Finale.AI
{
    public static class NeuralLogger
    {
        private static readonly string LogPath = "AppData/JaysAi/Finale/NeuralLog.txt";
        private static readonly List<string> _buffer = new();
        private static readonly object _lock = new();

        public static bool Enabled { get; set; } = true;
        public static int MaxLines { get; set; } = 1000;

        public static void Log(string message)
        {
            if (!Enabled) return;

            lock (_lock)
            {
                string timestamped = $"[{DateTime.UtcNow:HH:mm:ss.fff}] {message}";
                _buffer.Add(timestamped);

                if (_buffer.Count > MaxLines)
                    _buffer.RemoveAt(0);
            }
        }

        public static void Save()
        {
            try
            {
                lock (_lock)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(LogPath)!);
                    File.WriteAllLines(LogPath, _buffer);
                }
            }
            catch
            {
                // Logging failure shouldn't crash app
            }
        }

        public static void Clear()
        {
            lock (_lock)
            {
                _buffer.Clear();
                if (File.Exists(LogPath))
                    File.Delete(LogPath);
            }
        }
    }
}
