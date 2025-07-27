// Neural v3.1 — KeybindWatcherExtensions.cs
using System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public static class KeybindWatcherExtensions
    {
        public static string GetKeyDisplayName(this Key key)
        {
            return key == Key.None ? "Unbound" : key.ToString();
        }

        public static bool IsAssigned(this Key key)
        {
            return key != Key.None;
        }
    }
}
