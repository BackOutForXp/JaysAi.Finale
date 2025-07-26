// neural v3.0
using System;
using System.IO;
using System.Text;

namespace JaysAi.Finale.SystemLogic
{
    public static class FileHelper
    {
        public static bool FileExists(string path)
        {
            return !string.IsNullOrWhiteSpace(path) && File.Exists(path);
        }

        public static bool DirectoryExists(string path)
        {
            return !string.IsNullOrWhiteSpace(path) && Directory.Exists(path);
        }

        public static void SafeWriteAllText(string path, string content)
        {
            try
            {
                EnsureDirectoryExists(Path.GetDirectoryName(path));
                File.WriteAllText(path, content, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, $"SafeWriteAllText: {path}");
            }
        }

        public static void SafeAppendText(string path, string content)
        {
            try
            {
                EnsureDirectoryExists(Path.GetDirectoryName(path));
                File.AppendAllText(path, content + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, $"SafeAppendText: {path}");
            }
        }

        public static string SafeReadAllText(string path)
        {
            try
            {
                return FileExists(path) ? File.ReadAllText(path, Encoding.UTF8) : string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, $"SafeReadAllText: {path}");
                return string.Empty;
            }
        }

        public static void EnsureDirectoryExists(string? dir)
        {
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c.ToString(), "_");

            return name;
        }

        public static void DeleteFile(string path)
        {
            try
            {
                if (FileExists(path))
                    File.Delete(path);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, $"DeleteFile: {path}");
            }
        }
    }
}
