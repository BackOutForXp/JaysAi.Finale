using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class BoneData
    {
        public Vector3 Head { get; set; } = Vector3.Zero;
        public Vector3 Chest { get; set; } = Vector3.Zero;
        public Vector3 Stomach { get; set; } = Vector3.Zero;
        public Vector3 Feet { get; set; } = Vector3.Zero;

        public bool HasValidBones =>
            Head != Vector3.Zero &&
            Chest != Vector3.Zero;

        public Vector3 GetPrimaryTarget(BoneTarget targetPreference)
        {
            return targetPreference switch
            {
                BoneTarget.Head => Head,
                BoneTarget.Chest => Chest,
                BoneTarget.Stomach => Stomach,
                BoneTarget.Feet => Feet,
                _ => Chest
            };
        }
    }

    public enum BoneTarget
    {
        Head,
        Chest,
        Stomach,
        Feet
    }
}
