// neural v3.0
using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace JaysAi.Finale.AI
{
    public class ZoneManager
    {
        private readonly Size frameSize;
        private readonly double fieldOfViewRadius;
        private readonly Point2f screenCenter;
        private readonly double minimumConfidence;

        public ZoneManager(Size frameSize, double fovRadius = 160, double confidenceThreshold = 0.5)
        {
            this.frameSize = frameSize;
            fieldOfViewRadius = fovRadius;
            screenCenter = new Point2f(frameSize.Width / 2f, frameSize.Height / 2f);
            minimumConfidence = confidenceThreshold;
        }

        public bool IsTargetInFov(YoloBoundingBox target)
        {
            if (target.Confidence < minimumConfidence)
                return false;

            var center = target.GetCenter();
            return Distance(center, screenCenter) <= fieldOfViewRadius;
        }

        public List<YoloBoundingBox> FilterByFov(List<YoloBoundingBox> targets)
        {
            var validTargets = new List<YoloBoundingBox>();
            foreach (var target in targets)
            {
                if (IsTargetInFov(target))
                    validTargets.Add(target);
            }
            return validTargets;
        }

        private static double Distance(Point2f a, Point2f b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
