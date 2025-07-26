// neural v3.0
using System;
using System.IO;

namespace JaysAi.Finale.SystemLogic
{
    public static class FilePathHelper
    {
        public static string GetAppRoot()
        {
            return AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
        }

        public static string Combine(params string[] parts)
        {
            return Path.Combine(parts);
        }

        public static string GetFullPath(string relativePath)
        {
            if (Path.IsPathRooted(relativePath))
                return relativePath;

            return Path.Combine(GetAppRoot(), relativePath);
        }

        public static string GetTempPath(string fileName = "")
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "JaysAi", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(tempPath)!);
            return tempPath;
        }

        public static string GetUserDataPath(string subfolder = "")
        {
            string userPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "JaysAi", subfolder);

            Directory.CreateDirectory(userPath);
            return userPath;
        }

        public static string GetTimestampedFilePath(string baseDirectory, string prefix, string extension = ".log")
        {
            string fileName = $"{prefix}_{DateTime.UtcNow:yyyyMMdd_HHmmss}{extension}";
            return Path.Combine(baseDirectory, fileName);
        }

        public static string Normalize(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        public static string GetSafeFilename(string name, string extension)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c.ToString(), "_");

            return $"{name}{extension}";
        }
    }
}
