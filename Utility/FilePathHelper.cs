//monarch v2.1 – File Path Helper (Centralized Pathing)
using System.IO;

namespace JaysAi.Finale.Utility
{
    public static class FilePathHelper
    {
        private static readonly string BaseDirectory = Directory.GetCurrentDirectory();

        // Configs & Profiles
        public static string ConfigDirectory => Path.Combine(BaseDirectory, "Configs");
        public static string ProfilesDirectory => Path.Combine(BaseDirectory, "Profiles");

        // Logs
        public static string LogDirectory => Path.Combine(BaseDirectory, "Logs");
        public static string CrashLog => Path.Combine(LogDirectory, "crashlog.txt");

        // Models & AI Assets
        public static string ModelDirectory => Path.Combine(BaseDirectory, "Assets", "Models");
        public static string YoloWeights => Path.Combine(ModelDirectory, "yolov8n.onnx");

        // Screenshots, debug output
        public static string ScreenshotDirectory => Path.Combine(BaseDirectory, "Screenshots");

        // Temp or runtime paths
        public static string TempDirectory => Path.Combine(BaseDirectory, "Temp");

        // Create all folders on boot
        public static void EnsureDirectoriesExist()
        {
            Directory.CreateDirectory(ConfigDirectory);
            Directory.CreateDirectory(ProfilesDirectory);
            Directory.CreateDirectory(LogDirectory);
            Directory.CreateDirectory(ModelDirectory);
            Directory.CreateDirectory(ScreenshotDirectory);
            Directory.CreateDirectory(TempDirectory);
        }
    }
}
