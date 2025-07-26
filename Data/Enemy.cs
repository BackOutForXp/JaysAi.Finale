// neural v3.0
using System;
using System.Numerics;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.Data
{
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Vector3 HeadPosition { get; set; }
        public Vector3 ChestPosition { get; set; }
        public Vector3 FeetPosition { get; set; }
        public Vector2 ScreenPosition { get; set; }
        public float Distance { get; set; }
        public bool IsVisible { get; set; }
        public bool IsTracked { get; set; }
        public bool IsTargeted { get; set; }
        public float ConfidenceScore { get; set; }

        public TargetInfo TargetInfo { get; set; }
        public BoneData BoneData { get; set; }

        public DateTime LastSeen { get; set; }

        public Enemy()
        {
            TargetInfo = new TargetInfo();
            BoneData = new BoneData();
            LastSeen = DateTime.Now;
        }

        public void UpdatePosition(Vector3 head, Vector3 chest, Vector3 feet)
        {
            HeadPosition = head;
            ChestPosition = chest;
            FeetPosition = feet;
            LastSeen = DateTime.Now;
        }

        public void UpdateScreen(Vector2 screenPos, float distance)
        {
            ScreenPosition = screenPos;
            Distance = distance;
        }

        public void ResetFlags()
        {
            IsVisible = false;
            IsTracked = false;
            IsTargeted = false;
        }

        public override string ToString()
        {
            return $"Enemy[{Id}] {Name} | Dist: {Distance:F1}m | Visible: {IsVisible}";
        }
    }
}
