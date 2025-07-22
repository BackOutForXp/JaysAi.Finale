//heavenly v3.0.0 – Bone Index Mapping for Targeting
namespace JaysAi.Finale.AI
{
    public static class BoneData
    {
        public const int Head = 0;
        public const int Neck = 1;
        public const int Chest = 2;
        public const int Spine = 3;
        public const int Pelvis = 4;
        public const int LeftShoulder = 5;
        public const int RightShoulder = 6;
        public const int LeftElbow = 7;
        public const int RightElbow = 8;
        public const int LeftHand = 9;
        public const int RightHand = 10;
        public const int LeftKnee = 11;
        public const int RightKnee = 12;
        public const int LeftFoot = 13;
        public const int RightFoot = 14;

        public static string GetBoneName(int index)
        {
            return index switch
            {
                Head => "Head",
                Neck => "Neck",
                Chest => "Chest",
                Spine => "Spine",
                Pelvis => "Pelvis",
                LeftShoulder => "LShoulder",
                RightShoulder => "RShoulder",
                LeftElbow => "LElbow",
                RightElbow => "RElbow",
                LeftHand => "LHand",
                RightHand => "RHand",
                LeftKnee => "LKnee",
                RightKnee => "RKnee",
                LeftFoot => "LFoot",
                RightFoot => "RFoot",
                _ => "Unknown"
            };
        }
    }
}
