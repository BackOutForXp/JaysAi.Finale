// Neural v3.0 — ResourceHelper.cs
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace JaysAi.Finale.Helpers
{
    public static class ResourceHelper
    {
        public static string GetEmbeddedResourceText(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fullResourceName = assembly.GetManifestResourceNames()
                                           .FirstOrDefault(r => r.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase));

            if (fullResourceName == null)
                throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");

            using Stream stream = assembly.GetManifestResourceStream(fullResourceName);
            if (stream == null)
                throw new InvalidOperationException($"Failed to load resource stream for '{fullResourceName}'.");

            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }

        public static BitmapImage LoadBitmapFromResource(string resourcePath)
        {
            var uri = new Uri(resourcePath, UriKind.RelativeOrAbsolute);
            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = uri;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
    }
}
