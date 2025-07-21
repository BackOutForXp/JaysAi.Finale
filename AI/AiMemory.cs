//monarch v2.1 – AI Memory Snapshot
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public static class AiMemory
    {
        private static List<DetectionSnapshot> _history = new();

        public static void AddSnapshot(List<DetectionSnapshot> snapshot)
        {
            if (_history.Count >= 100)
                _history.RemoveAt(0);

            _history.Add(new DetectionSnapshot(snapshot));
        }

        public static List<DetectionSnapshot> GetHistory()
        {
            return new List<DetectionSnapshot>(_history);
        }

        public static void Clear()
        {
            _history.Clear();
        }
    }

    public class DetectionSnapshot
    {
        public List<DetectionObject> Objects { get; set; }

        public DetectionSnapshot(List<DetectionObject> objects)
        {
            Objects = new List<DetectionObject>(objects);
        }
    }

    public class DetectionObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool IsEnemy { get; set; }
    }
}
