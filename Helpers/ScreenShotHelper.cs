// Neural v3.0 — ScreenshotHelper.cs
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace JaysAi.Finale.Helpers
{
    public static class ScreenshotHelper
    {
        public static string CaptureFullScreen(string savePath = null)
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            using Bitmap bmp = new(screenBounds.Width, screenBounds.Height);
            using Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(Point.Empty, Point.Empty, screenBounds.Size);

            string filePath = savePath ?? GenerateFilePath();
            bmp.Save(filePath, ImageFormat.Png);
            return filePath;
        }

        public static string CaptureRegion(Rectangle region, string savePath = null)
        {
            using Bitmap bmp = new(region.Width, region.Height);
            using Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(region.Location, Point.Empty, region.Size);

            string filePath = savePath ?? GenerateFilePath();
            bmp.Save(filePath, ImageFormat.Png);
            return filePath;
        }

        private static string GenerateFilePath()
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "JaysAi");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        }
    }
}
