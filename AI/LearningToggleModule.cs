namespace JaysAi.Finale.AI
{
    public static class LearningToggleModule
    {
        // Global flag for enabling/disabling adaptive AI
        public static bool Enabled { get; private set; } = true;

        public static void Enable() => Enabled = true;
        public static void Disable() => Enabled = false;
        public static void Toggle() => Enabled = !Enabled;

        public static string Status => Enabled ? "Self-Learning AI: ON" : "Self-Learning AI: OFF";
    }
}
