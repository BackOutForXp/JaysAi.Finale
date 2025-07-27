// Neural v3.1 — TeamId.cs
namespace JaysAi.Finale.Data
{
    public class TeamId
    {
        public int Value { get; }

        public TeamId(int value)
        {
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is TeamId other && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(TeamId? a, TeamId? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Value == b.Value;
        }

        public static bool operator !=(TeamId? a, TeamId? b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"Team {Value}";
        }
    }
}
