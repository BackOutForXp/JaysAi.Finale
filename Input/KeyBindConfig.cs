//monarch v2.1 – Individual keybind config class

using global::System.Windows.Input;

namespace JaysAi.Finale.Input
{
    public class KeyBindConfig
    {
        public string Name { get; set; }
        public Key Key { get; set; }
        public bool ToggleMode { get; set; }

        public KeyBindConfig(string name, Key key, bool toggleMode = false)
        {
            Name = name;
            Key = key;
            ToggleMode = toggleMode;
        }
    }
}
