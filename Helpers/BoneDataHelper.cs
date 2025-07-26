// Neural v3.0 — BoneDataHelper.cs
using System.Collections.Generic;
using JaysAi.Finale.Visuals.Structs;

namespace JaysAi.Finale.Helpers
{
    public static class BoneDataHelper
    {
        /// <summary>
        /// Maps bone names to their visual render priority or groupings.
        /// Used by ESP drawing systems and hitbox prediction logic.
        /// </summary>
        public static readonly Dictionary<string, BoneVisualInfo> BoneMapping = new()
        {
            { "head",       new BoneVisualInfo("Head",       true, 1) },
            { "neck",       new BoneVisualInfo("Neck",       true, 2) },
            { "spine",      new BoneVisualInfo("Spine",      true, 3) },
            { "pelvis",     new BoneVisualInfo("Pelvis",     false, 4) },
            { "left_shoulder", new BoneVisualInfo("L_Shoulder", false, 5) },
            { "right_shoulder", new BoneVisualInfo("R_Shoulder", false, 5) },
            { "left_elbow", new BoneVisualInfo("L_Elbow",    false, 6) },
            { "right_elbow", new BoneVisualInfo("R_Elbow",   false, 6) },
            { "left_hand",  new BoneVisualInfo("L_Hand",     false, 7) },
            { "right_hand", new BoneVisualInfo("R_Hand",     false, 7) },
            { "left_knee",  new BoneVisualInfo("L_Knee",     false, 8) },
            { "right_knee", new BoneVisualInfo("R_Knee",     false, 8) },
            { "left_foot",  new BoneVisualInfo("L_Foot",     false, 9) },
            { "right_foot", new BoneVisualInfo("R_Foot",     false, 9) }
        };

        /// <summary>
        /// Checks if the bone exists in the current map.
        /// </summary>
        public static bool HasBone(string boneName) => BoneMapping.ContainsKey(boneName);

        /// <summary>
        /// Gets info about a bone if it exists.
        /// </summary>
        public static BoneVisualInfo GetBoneInfo(string boneName) =>
            BoneMapping.TryGetValue(boneName, out var info) ? info : BoneVisualInfo.Default;
    }
}
