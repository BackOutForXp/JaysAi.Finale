// Monarch v1.0 – CaptureEngine.cs
// ✅ Monarch Fix Checklist
// [x] Implements IFrameSource
// [x] Captures screen to OpenCV Mat
// [x] Uses OpenCvSharp + BitBlt screen capture

using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace JaysAi.Finale.AI
{
    public class CaptureEngine : IFrameSource
    {
        private const int CaptureWidth = 1920;
        private const int CaptureHeight = 1080;

        public Mat GetFrame()
        {
            using Bitmap bitmap = new(CaptureWidth, CaptureHeight, PixelFormat.Format24bppRgb);
            using Graphics g = Graphics.FromImage(bitmap);

            g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(CaptureWidth, CaptureHeight));
            return bitmap.ToMat();
        }
    }
}
