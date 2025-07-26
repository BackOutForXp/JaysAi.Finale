// neural v3.0
using System;
using System.IO;

namespace JaysAi.Finale.SystemLogic
{
    public static class UserDirectoryResolver
    {
        /// <summary>
        /// Gets the main JaysAi data directory under the user's AppData\Roaming folder.
        /// </summary>
        public static string GetAppDataDirectory()
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string finalPath = Path.Combine(basePath, "JaysAi");

            EnsureDirectoryExists(finalPath);
            return finalPath;
        }

        /// <summary>
        /// Gets a custom directory path under the JaysAi AppData folder.
        /// </summary>
        public static string GetCustomSubdirectory(string subfolderName)
        {
            string baseDir = GetAppDataDirectory();
            string subDir = Path.Combine(baseDir, subfolderName);

            EnsureDirectoryExists(subDir);
            return subDir;
        }

        /// <summary>
        /// Ensures the given directory path exists by creating it if it doesn't.
        /// </summary>
        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Gets the full path for a file inside a specific subdirectory.
        /// </summary>
        public static string GetFilePath(string subfolderName, string fileName)
        {
            string folder = GetCustomSubdirectory(subfolderName);
            return Path.Combine(folder, fileName);
        }
    }
}
