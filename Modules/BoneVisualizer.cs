// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Visuals.Overlay;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace JaysAi.Finale.Visuals
{
    public class BoneVisualizer
    {
        private readonly IOverlayRenderer _renderer;

        public BoneVisualizer(IOverlayRenderer renderer)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public void DrawBones(List<Keypoint> keypoints, Color color, float thickness = 1.5f)
        {
            if (keypoints == null || keypoints.Count < 17)
                return;

            // COCO 17 keypoints format bone connections
            int[,] bonePairs = new int[,]
            {
                {5, 6}, {5, 7}, {7, 9}, {6, 8}, {8, 10}, // Arms
                {11, 12}, {11, 13}, {13, 15}, {12, 14}, {14, 16}, // Legs
                {0, 1}, {1, 2}, {2, 3}, {3, 4}, {1, 5}, {1, 6}, {5, 11}, {6, 12} // Torso and base
            };

            for (int i = 0; i < bonePairs.GetLength(0); i++)
            {
                int startIdx = bonePairs[i, 0];
                int endIdx = bonePairs[i, 1];

                if (IsValidKeypoint(keypoints[startIdx]) && IsValidKeypoint(keypoints[endIdx]))
                {
                    Point start = new Point(keypoints[startIdx].X, keypoints[startIdx].Y);
                    Point end = new Point(keypoints[endIdx].X, keypoints[endIdx].Y);
                    _renderer.DrawLine(start, end, color, thickness);
                }
            }
        }

        private bool IsValidKeypoint(Keypoint kp)
        {
            return kp.Confidence > 0.3f && kp.X > 0 && kp.Y > 0;
        }
    }
}
