// Neural v3.1 — BoneData.cs
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.AI
{
    public enum BoneType
    {
        Head,
        Neck,
        Chest,
        Spine,
        Pelvis,
        LeftShoulder,
        RightShoulder,
        LeftArm,
        RightArm,
        LeftLeg,
        RightLeg,
        Unknown
    }

    public class BoneData
    {
        public int ID { get; set; }
        public BoneType Type { get; set; }
        public Vector3 Position { get; set; }
        public Vector2 ScreenPosition { get; set; }

        public BoneData(int id, BoneType type, Vector3 pos, Vector2 screenPos)
        {
            ID = id;
            Type = type;
            Position = pos;
            ScreenPosition = screenPos;
        }

        public bool IsCritical => Type == BoneType.Head || Type == BoneType.Chest;
    }

    public static class BoneMap
    {
        public static readonly Dictionary<BoneType, int> DefaultBoneIDs = new()
        {
            { BoneType.Head, 8 },
            { BoneType.Neck, 7 },
            { BoneType.Chest, 6 },
            { BoneType.Spine, 5 },
            { BoneType.Pelvis, 4 },
            { BoneType.LeftShoulder, 14 },
            { BoneType.RightShoulder, 15 },
            { BoneType.LeftArm, 16 },
            { BoneType.RightArm, 17 },
            { BoneType.LeftLeg, 18 },
            { BoneType.RightLeg, 19 }
        };

        public static BoneType GetBoneTypeByID(int id)
        {
            foreach (var pair in DefaultBoneIDs)
            {
                if (pair.Value == id)
                    return pair.Key;
            }
            return BoneType.Unknown;
        }
    }
}
