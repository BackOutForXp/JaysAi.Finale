// Monarch v1.0 – IFrameSource.cs
// ✅ Monarch Fix Checklist
// [x] Interface for frame grabbers
// [x] Supports screen or video capture
// [x] Works with OpenCVSharp Mat

using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public interface IFrameSource
    {
        Mat GetFrame();
    }
}
