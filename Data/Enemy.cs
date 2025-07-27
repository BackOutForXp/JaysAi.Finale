using System.Numerics;

namespace JaysAi.Finale.AI
{
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector2 ScreenPosition { get; set; }

        public float Distance { get; set; }
        public float Health { get; set; }

        public BoneData Bones { get; set; } = new();
        public bool IsVisible { get; set; }
        public bool IsAlive { get; set; }

        public MotionSample LastMotion { get; set; }

        public Enemy()
        {
            Position = Vector3.Zero;
            Velocity = Vector3.Zero;
            ScreenPosition = Vector2.Zero;
            IsVisible = false;
            IsAlive = false;
            Health = 100f;
            Distance = 0f;
        }

        public bool IsValid()
        {
            return IsAlive && Position != Vector3.Zero && Health > 0;
        }
    }
}
