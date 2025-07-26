//neural v3.0
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace JaysAi.Finale.Integration
{
    public class CaptureCardHelper : IDisposable
    {
        private VideoCapture? _capture;
        private bool _isInitialized;

        public bool Initialize(int captureIndex = 0, int width = 1920, int height = 1080, int fps = 60)
        {
            _capture = new VideoCapture(captureIndex, VideoCaptureAPIs.ANY);

            if (!_capture.IsOpened())
            {
                _isInitialized = false;
                return false;
            }

            _capture.Set(VideoCaptureProperties.FrameWidth, width);
            _capture.Set(VideoCaptureProperties.FrameHeight, height);
            _capture.Set(VideoCaptureProperties.Fps, fps);

            _isInitialized = true;
            return true;
        }

        public Mat? GetFrame()
        {
            if (!_isInitialized || _capture == null)
                return null;

            var frame = new Mat();
            return _capture.Read(frame) && !frame.Empty() ? frame : null;
        }

        public Bitmap? GetBitmapFrame()
        {
            var mat = GetFrame();
            return mat != null ? BitmapConverter.ToBitmap(mat) : null;
        }

        public void Dispose()
        {
            _capture?.Release();
            _capture?.Dispose();
            _capture = null;
            _isInitialized = false;
        }
    }
}
