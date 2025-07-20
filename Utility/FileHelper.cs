//monarch v1.0
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JaysAi.Finale.Utility
{
    public static class FileHelper
    {
        public static List<string> ReadLines(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<string>();

            return File.ReadAllLines(filePath).ToList();
        }

        public static void WriteLines(string filePath, IEnumerable<string> lines, bool append = false)
        {
            if (append)
                File.AppendAllLines(filePath, lines);
            else
                File.WriteAllLines(filePath, lines);
        }

        public static void CopyFile(string sourcePath, string destPath, bool overwrite = true)
        {
            if (!File.Exists(sourcePath)) return;
            File.Copy(sourcePath, destPath, overwrite);
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static string GetDirectoryName(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        public static string[] GetFilesInDirectory(string directoryPath, string searchPattern = "*.*")
        {
            if (!Directory.Exists(directoryPath))
                return Array.Empty<string>();

            return Directory.GetFiles(directoryPath, searchPattern);
        }
    }
}
