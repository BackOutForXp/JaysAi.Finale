// heavenly v3.0 – Enemy Model Representation
namespace JaysAi.Finale.Data
{
    public class Enemy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Distance { get; set; }
        public float Health { get; set; }
        public bool IsVisible { get; set; }
        public bool IsTargeted { get; set; }

        public float LastSeenTime { get; set; }
        public float MovementSpeed { get; set; }
        public string Team { get; set; }

        public Enemy(int id, string name, float distance, float health, bool isVisible, string team = "Unknown")
        {
            ID = id;
            Name = name;
            Distance = distance;
            Health = health;
            IsVisible = isVisible;
            IsTargeted = false;
            Team = team;
            LastSeenTime = 0f;
            MovementSpeed = 0f;
        }
    }
}
