// Neural v3.0 — TeamColorDetector.cs
using OpenCvSharp;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Modules
{
    public class TeamColorDetector
    {
        // Threshold ranges for each team color in HSV space
        private readonly Scalar EnemyMin = new Scalar(0, 100, 100);
        private readonly Scalar EnemyMax = new Scalar(10, 255, 255);

        private readonly Scalar AllyMin = new Scalar(100, 150, 80);
        private readonly Scalar AllyMax = new Scalar(130, 255, 255);

        private readonly Scalar SquadMin = new Scalar(30, 150, 70);
        private readonly Scalar SquadMax = new Scalar(60, 255, 255);

        public enum TeamColor
        {
            Unknown,
            Enemy,
            Ally,
            Squad
        }

        /// <summary>
        /// Returns the detected team based on a sampled region in a frame.
        /// </summary>
        public TeamColor DetectTeam(Mat frame, Rect sampleRegion)
        {
            if (frame == null || sampleRegion.Width == 0 || sampleRegion.Height == 0)
                return TeamColor.Unknown;

            try
            {
                using var roi = new Mat(frame, sampleRegion);
                using var hsv = new Mat();
                Cv2.CvtColor(roi, hsv, ColorConversionCodes.BGR2HSV);

                // Check against each range
                if (MatchesColorRange(hsv, EnemyMin, EnemyMax))
                    return TeamColor.Enemy;
                if (MatchesColorRange(hsv, AllyMin, AllyMax))
                    return TeamColor.Ally;
                if (MatchesColorRange(hsv, SquadMin, SquadMax))
                    return TeamColor.Squad;

                return TeamColor.Unknown;
            }
            catch
            {
                return TeamColor.Unknown;
            }
        }

        private bool MatchesColorRange(Mat hsvRegion, Scalar lower, Scalar upper)
        {
            using var mask = new Mat();
            Cv2.InRange(hsvRegion, lower, upper, mask);
            int nonZero = Cv2.CountNonZero(mask);
            return nonZero > (hsvRegion.Rows * hsvRegion.Cols * 0.05); // 5% pixel threshold
        }
    }
}
