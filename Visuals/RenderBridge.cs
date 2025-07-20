//monarch v2.1
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Visuals
{
    public interface IRenderBackend
    {
        void DrawBox(float x, float y, float width, float height, string label = null);
        void DrawText(float x, float y, string text);
        void DrawCircle(float centerX, float centerY, float radius);
        void Clear();
        void Present();
    }

    public class RenderBridge
    {
        private IRenderBackend backend;

        public RenderBridge(IRenderBackend initialBackend)
        {
            backend = initialBackend;
        }

        public void SetBackend(IRenderBackend newBackend)
        {
            backend = newBackend;
        }

        public void DrawBox(float x, float y, float width, float height, string label = null)
        {
            backend.DrawBox(x, y, width, height, label);
        }

        public void DrawText(float x, float y, string text)
        {
            backend.DrawText(x, y, text);
        }

        public void DrawCircle(float x, float y, float radius)
        {
            backend.DrawCircle(x, y, radius);
        }

        public void ClearAndPresent()
        {
            backend.Clear();
            backend.Present();
        }
    }
}
